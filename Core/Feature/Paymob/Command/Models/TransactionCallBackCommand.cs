using Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Paymob.Command.Models
{
    public  class TransactionCallBackCommand:IRequest<Response<string>>
    {
        public string? Hmac { get; set; }

        public CallbackObj Obj { get; set; } = default!;
    }
    public class CallbackObj
    {
        public long id { get; set; }

        public bool success { get; set; }

        public bool pending { get; set; }

        public decimal amount_cents { get; set; }

        public string currency { get; set; } = default!;

        public string created_at { get; set; } = default!;

        public CallbackOrder order { get; set; } = default!;

        public CallbackSourceData source_data { get; set; } = default!;

        public bool error_occured { get; set; }

        public bool has_parent_transaction { get; set; }

        public bool is_3d_secure { get; set; }

        public bool is_auth { get; set; }

        public bool is_capture { get; set; }

        public bool is_refunded { get; set; }

        public bool is_standalone_payment { get; set; }

        public bool is_voided { get; set; }

        public int integration_id { get; set; }

        public int owner { get; set; }
    }
    public class CallbackOrder
    {
        public long id { get; set; }

        public string? merchant_order_id { get; set; }
    }
    public class CallbackSourceData
    {
        public string? pan { get; set; }

        public string? type { get; set; }

        public string? sub_type { get; set; }
    }
}

