using Core.Feature.ApplicationDriver.Query.Models;
using Core.Feature.ApplicationDriver.Query.Result;
using Core.Feature.Buses.Command.Models;
using Core.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusBookingSystem.Controllers
{
    
    public class BusController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Pagination<GetAllBusesQueryResult>>>GetAllBuses([FromQuery]GetAllBusesQuery query)
        {
            var result =await _Mediator.Send(query);
            return NewResult(result);
        }

    }
}
