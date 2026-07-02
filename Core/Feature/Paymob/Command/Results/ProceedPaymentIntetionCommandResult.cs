using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Paymob.Command.Results
{
    public  class ProceedPaymentIntetionCommandResult
    {
        
        public int  PaymentId  { get; set; }
        public bool WasCachedResponse { get; set; }
        public string ClientURL { get; set; } = string.Empty;
    }
}
