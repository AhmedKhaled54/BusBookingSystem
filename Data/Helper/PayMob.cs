using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Helper
{
    public  class PayMob
    {
        public string BaseUrl { get; set; } = default!;

        public string ApiKey { get; set; } = default!;

        public string SecretKey { get; set; } = default!;

        public string PublicKey { get; set; } = default!;

        public string HMAC { get; set; } = default!;
        public int CardIntegrationId { get; set; }
        public int WalletIntegrationId { get; set; }

        public string IframeId { get; set; } = default!;

        public string CallbackUrl { get; set; } = default!;

        public string RedirectUrl { get; set; } = default!;
    }
}
