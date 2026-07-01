using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public  class PaymobIntetion
    {
        public int Id { get; set; }
        public string Request   { get; set; }
        public string Response { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string SpecailRefrence { get; set; }
        public string? PaymobOrderId { get; set; }
        public string PaymobIntentionId { get; set; }
        public string ClientSecret { get; set; }
        public int? Expiration { get; set; }

        [ForeignKey(nameof(Payment))]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}
