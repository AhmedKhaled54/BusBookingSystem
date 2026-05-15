using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Payment
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EGP";
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime PayedAt { get; set; } = DateTime.UtcNow;
        public string PaymentMethos { get; set; }
        public string TransactionId { get; set; }
        public Booking Booking { get; set; }
    }
}
