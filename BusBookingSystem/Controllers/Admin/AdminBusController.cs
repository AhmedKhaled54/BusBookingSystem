using Core.Feature.ApplicationDriver.Query.Models;
using Core.Feature.ApplicationDriver.Query.Result;
using Core.Feature.Buses.Command.Models;
using Core.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BusBookingSystem.Controllers.Admin
{

    //[Authorize(Roles ="Admin")]
    public class AdminBusController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Pagination<GetAllBusesQueryResult>>> GetAllBuses([FromQuery] GetAllBusesQuery query)
        {
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<GetDeletedBusesQueryResult>>>GetAllBusesDeleted([FromQuery] GetDeletedBusesQuery query)
        {
            var result =await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBus([FromForm] AddBusCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }


        [HttpPut]
        public async Task<IActionResult>RestoreBus(int Id)
        {
            var cmd = new RestoreBusCommand(Id);
            var result =await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpPatch]
        public async Task<IActionResult>ChangeBusStatus([FromQuery]ChangeBusStatusCommand cmd)
        {
            var result= await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpDelete]
        [SwaggerOperation(Summary ="Hard Deleted ")]
        public async Task<IActionResult> DeleteBus(int BusId)
        {
            var cmd = new DeleteBusCommand(BusId);
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpDelete]
        [SwaggerOperation(Summary ="Soft Deleted")]
        public async Task<IActionResult> DeleteBusSoft(int BusId)
        {
            var cmd = new DeleteSoftBusCommand(BusId);
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }
    }
}
