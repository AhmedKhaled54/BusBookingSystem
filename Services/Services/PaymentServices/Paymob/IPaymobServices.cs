using Data.Dtos.PaymobDto;
using Data.Dtos.PaymobDto.CallBackDtos;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Services.PaymentServices.Paymob
{
    public  interface IPaymobServices
    {
        Task<IntentionResponseDto> CreateIntentionAsync(IntentionRequestDto request);
        string GetUnifiedCheckoutUrl(string ClientSecret);

        Task<PaymobIntetion> GetById(int Id);
        Task<PaymobIntetion> GetByPaymentId (int PaymentId);
        Task<PaymobIntetion> GetByPaymobOrderId (string  PaymobOrdertId);
        Task<PaymobIntetion> GetBySpecialRefrence (string  SpecialRefrence);
        Task AddAsync(PaymobIntetion paymobIntetion);
        bool Update(PaymobIntetion paymobIntetion);


        string ComputeHmac(PaymobCallBackData data);

        bool ValidateHmac(PaymobCallBackData data,string receivedHmac);
        Task<TransactionInquiryResponseDto> GetTransactionInquiryAsync(string paymobOrderId,string merchantOrderId);

    }
}
