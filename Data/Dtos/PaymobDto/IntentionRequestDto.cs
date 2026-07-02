using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.PaymobDto
{
    public  class IntentionRequestDto
    {
        public int amount { get; set; }

        public string currency { get; set; } = "EGP";

        public List<int> payment_methods { get; set; } = new();

        public List<Item> items { get; set; } = new();

        public BillingData billing_data { get; set; } = default!;

        public object extras { get; set; } = default!;

        public string special_reference { get; set; } = default!;

        public int expiration { get; set; } = 3600;

        public string notification_url { get; set; } = default!;

        public string redirection_url { get; set; } = default!;

    }


    public class Item
    {
        public string name { get; set; } = default!;
        public int amount { get; set; }
        public int quantity { get; set; }

    }
    public class BillingData
    {
        public string apartment { get; set; } = "dumy";

        public string floor { get; set; } = "dumy";

        public string first_name { get; set; } = default!;

        public string last_name { get; set; } = default!;

        public string street { get; set; } = "dumy";

        public string building { get; set; } = "dumy";

        public string phone_number { get; set; } = default!;

        public string city { get; set; } = "dumy";

        public string country { get; set; } = "EG";

        public string state { get; set; } = "dumy";

        public string email { get; set; } = default!;

        public string postal_code { get; set; } = "";
    }
}
