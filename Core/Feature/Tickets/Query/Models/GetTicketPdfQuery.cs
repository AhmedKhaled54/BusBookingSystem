using Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Tickets.Query.Models
{
    public  class GetTicketPdfQuery:IRequest<Response<byte[]>>
    {
        public int TicketId { get; set; }

        public GetTicketPdfQuery(int ticketId)
        {
            TicketId = ticketId;
        }
    }
}
