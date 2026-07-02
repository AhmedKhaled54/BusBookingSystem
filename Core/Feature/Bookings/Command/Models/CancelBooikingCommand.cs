
using Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Bookings.Command.Models
{
    public  class CancelBooikingCommand:IRequest<Response<bool>>
    {
        public int BookingId { get; set; }
        //public int userid { get; set; }
    }
}
