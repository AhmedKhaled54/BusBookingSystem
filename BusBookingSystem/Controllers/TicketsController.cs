using AutoMapper.Execution;
using Core.Feature.Tickets.Query.Models;
using Core.Feature.Tickets.Query.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusBookingSystem.Controllers
{
    public class TicketsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult>GetTickets (int BookindId)
        {
            var query = new GetTicketByBookingIdQuery(BookindId);
            var result =await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadPdf(int id)
        {
            var query = new GetTicketPdfQuery(id);
            var result = await _Mediator.Send(query);
            if (!result.Success)
                return NewResult(result);

            return File(
                result.Data,
                "application/pdf",
                $"ticket-{id}.pdf"
            );
        }
    }
}
