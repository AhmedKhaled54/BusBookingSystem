using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.PaymobDto
{
    public  class IntentionResponseDto
    {
        public List<PaymentKey> payment_keys { get; set; } = new();

        public long intention_order_id { get; set; }
        public string id { get; set; } = default!;

        public IntentionDetail intention_detail { get; set; } = default!;

        public string client_secret { get; set; } = default!;

        public List<PaymentMethod> payment_methods { get; set; } = new();

        public string special_reference { get; set; } = default!;

        public ExtrasResponse extras { get; set; } = default!;

        public bool confirmed { get; set; }

        public string status { get; set; } = default!;

        public DateTime created { get; set; }

        public object? card_detail { get; set; }

        public List<object> card_tokens { get; set; } = new();

        public string object_type { get; set; } = default!;
    }
    public class IntentionDetail
    {
        public int amount { get; set; }

        public string currency { get; set; } = default!;

        public List<Item> items { get; set; } = new();

        public BillingData billing_data { get; set; } = default!;
    }
    public class PaymentMethod
    {
        public int integration_id { get; set; }

        public string? alias { get; set; }

        public string name { get; set; } = default!;

        public string method_type { get; set; } = default!;

        public string currency { get; set; } = default!;

        public bool live { get; set; }

        public bool use_cvc_with_moto { get; set; }
    }
    public class ExtrasResponse
    {
        public CreationExtras creation_extras { get; set; } = default!;

        public object? confirmation_extras { get; set; }
    }

    public class CreationExtras
    {
        public int ee { get; set; }
    }
    public class PaymentKey
    {
        public int integration { get; set; }

        public string key { get; set; } = default!;

        public string gateway_type { get; set; } = default!;

        public int? iframe_id { get; set; }

        public long order_id { get; set; }
    }
}
