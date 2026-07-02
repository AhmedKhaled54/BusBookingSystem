using Core.Base;
using Core.Feature.Paymob.Command.Models;
using Core.Feature.Paymob.Command.Results;
using Data.Dtos.PaymobDto;
using Data.Dtos.PaymobDto.CallBackDtos;
using Data.Entity;
using Data.Enums;
using Data.Helper;
using Data.Identity;
using Infrastracture.Abstract;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Services.BookingService;
using Services.Services.PaymentServices.Payment;
using Services.Services.PaymentServices.Paymob;
using Services.Services.SeatsService;
using Services.Services.TicketService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Feature.Paymob.Command.Handler
{
    public class ProceedPaymentByIntentionIdCommandHandlet : ResponseHandler,
        IRequestHandler<ProceedPaymentByIntentionIdCommand, Response<ProceedPaymentIntetionCommandResult>>,
        IRequestHandler<TransactionCallBackCommand, Response<string>>
    {


        #region Feild 
        private readonly IBookingServices _bookingServices;
        private readonly IPaymobServices _paymobServices;
        private readonly IPaymentServices _paymentServices;
        private readonly PayMob _payMob;
        private readonly UserManager<User> _usermanager;
        private readonly IUnitOfWork _UOW;
        private readonly ISeatServices _SeatServices;
        private readonly ITicketServices _TicketServices;
        private readonly IHttpContextAccessor _httpContext;



        #endregion
        #region Ctor 
        public ProceedPaymentByIntentionIdCommandHandlet(IBookingServices bookingServices, IPaymobServices paymobServices, IPaymentServices paymentServices, PayMob payMob, UserManager<User> usermanager, IUnitOfWork uOW, ISeatServices seatServices, ITicketServices ticketServices, IHttpContextAccessor httpContext)
        {
            _bookingServices = bookingServices;
            _paymobServices = paymobServices;
            _paymentServices = paymentServices;
            _payMob = payMob;
            _usermanager = usermanager;
            _UOW = uOW;
            _SeatServices = seatServices;
            _TicketServices = ticketServices;
            _httpContext = httpContext;
        }
        #endregion
        public async Task<Response<ProceedPaymentIntetionCommandResult>> Handle(ProceedPaymentByIntentionIdCommand request, CancellationToken cancellationToken)
        {
            var  userid =GetUserId();
            if (userid==0)
                return UnAuthorize<ProceedPaymentIntetionCommandResult>("Incorrect Please Try Again !");
            //var user = await _UOW.Repository<User>().FindAsync(c => c.Id == request.userid);
            var user = await _usermanager.Users.FirstOrDefaultAsync(c => c.Id == userid);
            var booking = await _bookingServices.GetBookingById(request.BookingId);
            if (booking == null)
                return NotFound<ProceedPaymentIntetionCommandResult>("Booking Not Found! ");
            if (booking.Status == BookingStatus.Confirmed)
                return BadRequest<ProceedPaymentIntetionCommandResult>("Booking Already Paid ");
            var existingIntention = await _paymobServices.GetBySpecialRefrence(request.IdempotencyKey);
            if (existingIntention != null)
            {
                var exitresult = new ProceedPaymentIntetionCommandResult
                {
                    ClientURL = _paymobServices.GetUnifiedCheckoutUrl(existingIntention.ClientSecret),
                    PaymentId = existingIntention.PaymentId,
                    WasCachedResponse = true
                };
                return Success<ProceedPaymentIntetionCommandResult>(exitresult, "Payment intention already exists");
            }

            var exsitPayment = await _paymentServices.GetPaymentByBookingId(request.BookingId);
            if (exsitPayment != null)
                return BadRequest<ProceedPaymentIntetionCommandResult>("Payment Already Exsit ");

            var PaymentMethod = request.PaymentsMethod switch
            {
                PaymentsMethod.Card => _payMob.CardIntegrationId,
                PaymentsMethod.MobileWallet => _payMob.WalletIntegrationId,
                _ => throw new Exception("Invalid PaymentMethod! ")
            };

            var payment = new Payment
            {
                BookingId = booking.Id,
                Amount = booking.TotalPrice,
                Currency = "EGP",
                Status = PaymentStatus.Pending,
                CreatedAt = DateTime.Now,
                PaymentMethod = request.PaymentsMethod
            };

            var paymentresult = await _paymentServices.Add(payment);

            var IntentionRequest = new IntentionRequestDto
            {
                amount = (int)(booking.TotalPrice * 100),
                currency = "EGP",
                payment_methods = new List<int> { PaymentMethod },
                items = new List<Item>
                {
                    new()
                    {
                        amount=(int)(booking.TotalPrice*100),
                        name=$"{booking.Trip?.Routes?.StartStation?.Name??"null"}-{booking.Trip?.Routes?.EndStation?.Name??"null"}",
                        quantity=1
                    }
                },
                billing_data = new BillingData
                {
                    apartment = "dumy",
                    email = user.Email,
                    first_name = user.FirstName,
                    last_name = user.LastName,
                    phone_number = user.PhoneNumber,
                    city = "dumy",
                    country = "dumy",
                    floor = "dumy",
                    building = "dumy",
                    state = "dumy",
                    street = "dumy"
                },
                extras = new
                {
                    BookingId = booking.Id,
                    UserId = user.Id
                },
                special_reference = request.IdempotencyKey,
                expiration = 3600,
                notification_url = _payMob.CallbackUrl,
                redirection_url = _payMob.RedirectUrl

            };

            var intentionresponse = await _paymobServices.CreateIntentionAsync(IntentionRequest);
            if (intentionresponse != null && string.IsNullOrWhiteSpace(intentionresponse.client_secret))
                return BadRequest<ProceedPaymentIntetionCommandResult>("Failed to create payment intention");
            var PaymobIntentoin = new PaymobIntetion
            {
                SpecailRefrence = request.IdempotencyKey,
                PaymobOrderId = intentionresponse.intention_order_id.ToString(),
                PaymentId = paymentresult.Id,
                Request = JsonSerializer.Serialize(IntentionRequest),
                Response = JsonSerializer.Serialize(intentionresponse),
                PaymobIntentionId = intentionresponse.id,
                Expiration = IntentionRequest.expiration,
                ClientSecret = intentionresponse.client_secret
            };
            await _paymobServices.AddAsync(PaymobIntentoin);
            var checkoutUrl = _paymobServices.GetUnifiedCheckoutUrl(intentionresponse.client_secret);

            var result = new ProceedPaymentIntetionCommandResult
            {
                ClientURL = checkoutUrl,
                PaymentId = paymentresult.Id,
                WasCachedResponse = false
            };
            return Success<ProceedPaymentIntetionCommandResult>(result, "Payment intention created successfully");
        }

        public async Task<Response<string>> Handle(TransactionCallBackCommand request, CancellationToken cancellationToken)
        {
            var callbackData = new PaymobCallBackData
            {
                AmountCents = request.Obj.amount_cents,
                CreatedAt = request.Obj.created_at,
                Currency = request.Obj.currency,
                ErrorOccured = request.Obj.error_occured,
                HasParentTransaction = request.Obj.has_parent_transaction,
                TransactionId = request.Obj.id,
                IntegrationId = request.Obj.integration_id,
                Is3DSecure = request.Obj.is_3d_secure,
                IsAuth = request.Obj.is_auth,
                IsCapture = request.Obj.is_capture,
                IsRefunded = request.Obj.is_refunded,
                IsStandalonePayment = request.Obj.is_standalone_payment,
                IsVoided = request.Obj.is_voided,
                OrderId = request.Obj.order.id,
                Owner = request.Obj.owner,
                Pending = request.Obj.pending,
                Pan = request.Obj.source_data.pan ?? string.Empty,
                SubType = request.Obj.source_data.sub_type ?? string.Empty,
                Type = request.Obj.source_data.type ?? string.Empty,
                Success = request.Obj.success
            };

            var isValid = _paymobServices.ValidateHmac(
                callbackData,
                request.Hmac ?? string.Empty);

            if (!isValid)
                return BadRequest<string>("Invalid HMAC");

            var Inquery = await _paymobServices.GetTransactionInquiryAsync(request.Obj.order.id.ToString(), request.Obj.order.merchant_order_id);
            if (!Inquery.Success)
                BadRequest<string>("Payment failed");

            if (Inquery.Pending)
                BadRequest<string>("Payment pending");

            if (Inquery.IsVoided)
                BadRequest<string>("Payment voided");

            if (Inquery.IsRefunded)
                BadRequest<string>("Payment refunded");

            if (Inquery.ErrorOccured)
                BadRequest<string>("Payment error");

            var intention = await _paymobServices.GetBySpecialRefrence(request.Obj.order.merchant_order_id.ToString());
            var Payment = await _paymentServices.GetPaymentById(intention.PaymentId);
            var booking = await _bookingServices.GetBookingById(Payment.BookingId);
            Payment.Status = PaymentStatus.Success;
            Payment.TransactionId = request.Obj.id.ToString();
            Payment.PayedAt = DateTime.UtcNow;
            booking.Status = BookingStatus.Confirmed;
            foreach (var bookingseats in booking.BookingSeats)
            {
                bookingseats.Status = SeatsType.Confirmed;
            }
            await _SeatServices.ReleaseSeatsAsync(booking.TripId, booking.BookingSeats.Select(c => c.SeatsId).ToList());
            await _TicketServices.GenerateTicket(booking.Id, booking.BookingNumber);

            return Success("HMAC Validated Successfully");

        }

        private int GetUserId()
        {
            ClaimsPrincipal claim = _httpContext.HttpContext.User;
            var userid = int.Parse(_usermanager.GetUserId(claim));
            return userid;

        }
    }
}
