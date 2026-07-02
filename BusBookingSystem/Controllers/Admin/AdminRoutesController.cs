using Core.Feature.Route.Command.Models;
using Core.Feature.Route.Command.Validator;
using Core.Feature.Route.Query.Models;
using Core.Feature.Route.Query.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Tnef;

namespace BusBookingSystem.Controllers.Admin
{
    
    public class AdminRoutesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetRoutesQueryResult>>> GetAllRoutes()
        {
            var query = new GetAllRoutesQuery();
            var result =await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetRoutesQueryResult>> GetRouteById([FromQuery] int Id )
        {
            var query = new GetRouteByIdQuery(Id);
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoutes([FromQuery] CreateRouteCommand cmd)
        {
            var result=await _Mediator.Send(cmd);
            return NewResult(result);
        }
    }
}
