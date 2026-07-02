using Core.Feature.ApplicationDriver.Query.Models;
using Core.Feature.Trips.Command.Models;
using Core.Feature.Trips.Query.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusBookingSystem.Controllers
{

    public class TripController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetAllTrips([FromQuery] GetAllTripsQuery query)
        {
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTripById([FromQuery] int Id)
        {
            var query = new GetTripByIdQuery(Id);
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }

       
    }
}
