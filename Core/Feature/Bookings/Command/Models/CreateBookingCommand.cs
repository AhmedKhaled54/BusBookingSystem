using Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Booking.Command.Models
{
    public  class CreateBookingCommand:IRequest<Response<string>>
    {
        public int TripId { get; set; }
        public List<int> SeatsId { get; set; }
        //public int UserId { get; set; }
    }
}
