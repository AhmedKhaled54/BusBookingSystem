using Core.Base;
using Core.Feature.Paymob.Command.Results;
using Data.Dtos.PaymobDto;
using Data.Enums;
using MediatR;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Paymob.Command.Models
{
    public  class ProceedPaymentByIntentionIdCommand:IRequest<Response<ProceedPaymentIntetionCommandResult>>
    {
        public PaymentsMethod  PaymentsMethod { get; set; }
        public int BookingId { get; set; }
        public required string IdempotencyKey { get; set; }
        //public int userid { get; set; }

    }
}
