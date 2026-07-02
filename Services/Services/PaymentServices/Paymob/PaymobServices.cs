using Data.Dtos.PaymobDto;
using Data.Dtos.PaymobDto.CallBackDtos;
using Data.Entity;
using Data.Helper;
using Infrastracture.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Services.Services.PaymentServices.Paymob
{
    public class PaymobServices : IPaymobServices
    {
        private readonly PayMob _payMob;
        private readonly HttpClient _httpClient;
        private readonly IUnitOfWork _UOW;

        public PaymobServices(PayMob payMob, HttpClient httpClient, IUnitOfWork uOW)
        {
            _payMob = payMob;
            _httpClient = httpClient;
            _UOW = uOW;
        }


        public async Task<IntentionResponseDto> CreateIntentionAsync(IntentionRequestDto request)
        {

            var client = new HttpClient();
            var requests = new HttpRequestMessage(HttpMethod.Post, "https://accept.paymob.com/v1/intention/");
            requests.Headers.Add("Authorization", $"Token {_payMob.SecretKey}");
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            requests.Content = content;
            var response = await client.SendAsync(requests);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Paymob Error: {responseContent}");
            }
            response.EnsureSuccessStatusCode();
            var responsecontent = await response.Content.ReadAsStringAsync();
            var intentionresponse = JsonSerializer.Deserialize<IntentionResponseDto>(responsecontent);
            return intentionresponse ?? new IntentionResponseDto();
        }

        public async Task<PaymobIntetion> GetById(int Id)
            => await _UOW.Repository<PaymobIntetion>().GetByIdAsync(Id);

        public async Task<PaymobIntetion> GetByPaymentId(int PaymentId)
         => await _UOW.Repository<PaymobIntetion>().FindAsync(c => c.PaymentId == PaymentId);

        public async Task<PaymobIntetion> GetByPaymobOrderId(string PaymobOrdertId)
            => await _UOW.Repository<PaymobIntetion>().FindAsync(c => c.PaymobOrderId == PaymobOrdertId);


        public async  Task<PaymobIntetion> GetBySpecialRefrence(string SpecialRefrence)
            => await _UOW.Repository<PaymobIntetion>().FindAsync(c => c.SpecailRefrence == SpecialRefrence);


        public string GetUnifiedCheckoutUrl(string ClientSecret)
        => $"https://accept.paymob.com/unifiedcheckout/?publicKey={_payMob.PublicKey}&clientSecret={ClientSecret}";
        public async Task AddAsync(PaymobIntetion paymobIntetion)
            =>await _UOW.Repository<PaymobIntetion> ().AddAsync(paymobIntetion);

        public bool Update(PaymobIntetion paymobIntetion)
        {
            _UOW.Repository<PaymobIntetion>().Update(paymobIntetion);
            return true;
        }
        

        public string ComputeHmac(PaymobCallBackData data)
        {
            var sb = new StringBuilder();

            sb.Append(data.AmountCents);
            sb.Append(data.CreatedAt);
            sb.Append(data.Currency);
            sb.Append(data.ErrorOccured.ToString().ToLower());
            sb.Append(data.HasParentTransaction.ToString().ToLower());
            sb.Append(data.TransactionId);
            sb.Append(data.IntegrationId);
            sb.Append(data.Is3DSecure.ToString().ToLower());
            sb.Append(data.IsAuth.ToString().ToLower());
            sb.Append(data.IsCapture.ToString().ToLower());
            sb.Append(data.IsRefunded.ToString().ToLower());
            sb.Append(data.IsStandalonePayment.ToString().ToLower());
            sb.Append(data.IsVoided.ToString().ToLower());
            sb.Append(data.OrderId);
            sb.Append(data.Owner);
            sb.Append(data.Pending.ToString().ToLower());
            sb.Append(data.Pan);
            sb.Append(data.SubType);
            sb.Append(data.Type);
            sb.Append(data.Success.ToString().ToLower());

            var keyBytes = Encoding.UTF8.GetBytes(_payMob.HMAC);
            var dataBytes = Encoding.UTF8.GetBytes(sb.ToString());

            using var hmac = new HMACSHA512(keyBytes);

            var hashBytes = hmac.ComputeHash(dataBytes);

            return Convert.ToHexString(hashBytes).ToLower();
        }

        public bool ValidateHmac(PaymobCallBackData data, string receivedHmac)
        {
            var computedHmac = ComputeHmac(data);

            return string.Equals(computedHmac,receivedHmac,StringComparison.OrdinalIgnoreCase);
        }

        public async  Task<TransactionInquiryResponseDto> GetTransactionInquiryAsync(string paymobOrderId, string merchantOrderId)
        {
            var authToken = await AuthenticationRequest_GenerateAuthToken();

            if (string.IsNullOrWhiteSpace(authToken))
                throw new Exception("Failed to generate Paymob auth token.");

            using var client = new HttpClient();

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://accept.paymob.com/api/ecommerce/orders/transaction_inquiry");

            request.Content = new StringContent(
                JsonSerializer.Serialize(new
                {
                    auth_token = authToken,
                    PaymobOrder_id = paymobOrderId,
                    merchant_order_id = merchantOrderId
                }),Encoding.UTF8,"application/json");

            var response = await client.SendAsync(request);

            var responseContent =
                await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Paymob Inquiry Error : {responseContent}");
            }

            return JsonSerializer.Deserialize<TransactionInquiryResponseDto>(responseContent)?? 
                throw new Exception("Invalid inquiry response.");
        }


        private  async Task<string> AuthenticationRequest_GenerateAuthToken()
        {
            var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/auth/tokens",
                new { api_key = _payMob.ApiKey});

            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadFromJsonAsync<AuthResponse>();
            return result?.Token ?? string.Empty;
        }
    }
}
