using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.PaymobDto.CallBackDtos
{
    public  class PaymobCallBackData
    {
        public decimal AmountCents { get; set; }
        public string CreatedAt { get; set; } = default!;
        public string Currency { get; set; } = default!;
        public bool ErrorOccured { get; set; }
        public bool HasParentTransaction { get; set; }
        public long TransactionId { get; set; }
        public int IntegrationId { get; set; }
        public bool Is3DSecure { get; set; }
        public bool IsAuth { get; set; }
        public bool IsCapture { get; set; }
        public bool IsRefunded { get; set; }
        public bool IsStandalonePayment { get; set; }
        public bool IsVoided { get; set; }
        public long OrderId { get; set; }
        public long Owner { get; set; }
        public bool Pending { get; set; }
        public string Pan { get; set; } = default!;
        public string SubType { get; set; } = default!;
        public string Type { get; set; } = default!;
        public bool Success { get; set; }
    }
}
