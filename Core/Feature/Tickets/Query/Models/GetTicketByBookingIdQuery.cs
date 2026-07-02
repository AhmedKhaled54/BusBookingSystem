using Core.Base;
using Core.Feature.Tickets.Query.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Tickets.Query.Models
{
    public  class GetTicketByBookingIdQuery:IRequest<Response<GetTicketByBookingIdQueryResult>>
    {
        public int BookingId {  get; set; }

        public GetTicketByBookingIdQuery(int bookingId)
        {
            BookingId = bookingId;
        }
    }
}
