using Core.Base;
using Core.Feature.Booking.Command.Models;
using Core.Feature.Bookings.Command.Models;
using Data.Enums;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.Services.BookingService;
using Services.Services.SeatsService;
using Services.Services.TripsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Booking.Command.Handler
{
    public class AddBookingHandler : ResponseHandler,
        IRequestHandler<CreateBookingCommand, Response<string>>,
        IRequestHandler<CancelBooikingCommand, Response<bool>>
    {

        #region Feild
        private readonly IBookingServices _bookingServices;
        private readonly ISeatServices _seatServices;
        private readonly ITripeServices _tripServices;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<User> _usermanager;

        #endregion
        #region Ctor 
        public AddBookingHandler(IBookingServices bookingServices, ISeatServices seatServices, ITripeServices tripServices, IHttpContextAccessor httpContext, UserManager<User> usermanager)
        {
            _bookingServices = bookingServices;
            _seatServices = seatServices;
            _tripServices = tripServices;
            _bookingServices = bookingServices;
            _seatServices = seatServices;
            _httpContext = httpContext;
            _usermanager = usermanager;
        }
        #endregion
        public async Task<Response<string>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var userid = GetUserId();
            if (userid<=0)
                return UnAuthorize<string>("Incorrect Please Try Again ");

            var trip = await _tripServices.GetTripById(request.TripId);
            if (trip == null)
                return NotFound<string>("Trip Not Found");

            var locked = await _seatServices.TryLockSeatsAsync(request.TripId, request.SeatsId, userid);
            if (!locked)
                return BadRequest<string>("Some Seats Are Locked By Other Users");

            try
            {
                var availableSeats = await _seatServices.GetIvalaibleSeats(trip.BusId, request.SeatsId);
                if (availableSeats.Count() != request.SeatsId.Count)
                {
                    await _seatServices.ReleaseSeatsAsync(request.TripId, request.SeatsId, userid);
                    return BadRequest<string>("Some Seats Not Available");
                }
                var reversed = await _seatServices.SeatsReversed(request.TripId, request.SeatsId);
                if (reversed)
                {
                    await _seatServices.ReleaseSeatsAsync(request.TripId, request.SeatsId, userid);
                    return BadRequest<string>("Some Seats Reversed");
                }
                var booking = await _bookingServices.CreateBooking(request.TripId, userid, trip.Price, availableSeats.ToList());

                if (booking == null)
                {
                    await _seatServices.ReleaseSeatsAsync(request.TripId, request.SeatsId, userid);
                    return BadRequest<string>("Booking Failed");
                }

            }
            catch
            {

                await _seatServices.ReleaseSeatsAsync(request.TripId, request.SeatsId, userid);
            }
            return Success<string>("Booking Created Successfully");

        }

        public async Task<Response<bool>> Handle(CancelBooikingCommand request, CancellationToken cancellationToken)
        {
            var userid = GetUserId();
            if (userid <= 0)
                return UnAuthorize<bool>("Incorrect Please Try Again ");


            var booking = await _bookingServices.GetBookingById(request.BookingId);
            if (booking == null)
                return NotFound<bool>("Booking Not Found !");
            if (booking.UserId != userid)
                return UnAuthorize<bool>("You are not allowed to access this booking");

            if (booking.Status == BookingStatus.Cancelled)
                return BadRequest<bool>("Booking Already Cancelled ");

            if (booking.Payment?.Status == PaymentStatus.Success)
                return BadRequest<bool>("Cannot Cancel After Payment ");
            booking.Status = BookingStatus.Cancelled;
            foreach (var seat in booking.BookingSeats)
                seat.Status = SeatsType.Released;
            await _seatServices.ReleaseSeatsAsync(booking.TripId, booking.BookingSeats.Select(c => c.SeatsId).ToList(), userid);

            return Success(true);
        }

        private int GetUserId()
        {
            ClaimsPrincipal claim = _httpContext.HttpContext.User;
            var userid = int.Parse(_usermanager.GetUserId(claim));
            return userid;

        }
    }
}
