using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.PaymobDto.CallBackDtos
{
    public class TransactionInquiryResponseDto
    {
        public long Id { get; set; }

        public bool Success { get; set; }

        public bool Pending { get; set; }

        public decimal AmountCents { get; set; }

        public string Currency { get; set; } = string.Empty;

        public bool IsRefunded { get; set; }

        public bool IsVoided { get; set; }

        public bool IsCaptured { get; set; }

        public bool ErrorOccured { get; set; }

        public InquiryOrderDto Order { get; set; } = default!;
    }

    public class InquiryOrderDto
    {
        public long Id { get; set; }

        public string? MerchantOrderId { get; set; }
    }
}
