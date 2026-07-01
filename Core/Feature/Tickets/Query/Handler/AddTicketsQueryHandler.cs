using AutoMapper;
using Core.Base;
using Core.Feature.Tickets.Query.Models;
using Core.Feature.Tickets.Query.Results;
using MediatR;
using Services.Services.TicketService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Tickets.Query.Handler
{
    public class AddTicketsQueryHandler : ResponseHandler,
        IRequestHandler<GetTicketByBookingIdQuery, Response<GetTicketByBookingIdQueryResult>>,
        IRequestHandler<GetTicketPdfQuery, Response<byte[]>>
    {
        #region Feild
        private readonly ITicketServices _TicketServices;
        private readonly IMapper _Mapper;
        #endregion
        #region Ctor 
        public AddTicketsQueryHandler(ITicketServices ticketServices, IMapper mapper)
        {
            _TicketServices = ticketServices;
            _Mapper = mapper;
        }
        #endregion
        public async Task<Response<GetTicketByBookingIdQueryResult>> Handle(GetTicketByBookingIdQuery request, CancellationToken cancellationToken)
        {

            var ticket = await _TicketServices.GetTicketByBookingId(request.BookingId);
            if (ticket == null)
                return NotFound<GetTicketByBookingIdQueryResult>("Not Found Tickets ");
            var result = _Mapper.Map<GetTicketByBookingIdQueryResult>(ticket);
            return Success(result);
        }

        public async  Task<Response<byte[]>> Handle(GetTicketPdfQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _TicketServices.GetTicketById(request.TicketId);
            if (ticket == null)
                return NotFound<byte[]>("Not Found Ticket");
            var pdf =  _TicketServices.GeneratePdf(ticket);
            return Success(pdf);
        }
    }
}
