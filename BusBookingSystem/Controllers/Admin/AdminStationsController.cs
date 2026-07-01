using Core.Feature.Stations.Command.Models;
using Core.Feature.Stations.Query.Models;
using Core.Feature.Stations.Query.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusBookingSystem.Controllers.Admin
{
    public class AdminStationsController : BaseController
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllStationQueryResult>>>GetAllStation()
        {
            var query = new GetAllStationQuery();
            var result =await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetAllStationQueryResult>>GetStationById(int Id )
        {
            var query = new GetStationByIdQuery(Id );
            var result =await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedStation()
        {
            var query = new GetDeleteSataionQuery();
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult>CreateStation (CreateStationCommand cmd)
        {
            var result =await _Mediator.Send(cmd);
            return NewResult(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStation([FromQuery] UpdateStationCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);   
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSoftStation([FromQuery]DeletedSoftStationCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }
    }
}
