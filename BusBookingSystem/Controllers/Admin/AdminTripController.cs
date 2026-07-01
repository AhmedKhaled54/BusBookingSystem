using Core.Feature.Trips.Command.Models;
using Core.Feature.Trips.Query.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusBookingSystem.Controllers.Admin
{

    public class AdminTripController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetDeletedTrips([FromQuery] GetDeletedTripQuery query)
        {
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTrip([FromQuery] CreateTripCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateTrip([FromQuery] UpdateTripCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);

        }

        [HttpPatch]
        public async Task<IActionResult> ChangeTripStatus([FromQuery] ChangeTripStatusCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSoftTrip([FromQuery] int Id)
        {
            var cmd = new DeleteSoftTripCommand(Id);
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }
        [HttpPut]
        public async Task<IActionResult> RestoreTrip([FromQuery] int Id)
        {
            var cmd = new RestoreTripCommand(Id);
            var result = await _Mediator.Send(cmd);
            return NewResult(result);

        }
    }
}
