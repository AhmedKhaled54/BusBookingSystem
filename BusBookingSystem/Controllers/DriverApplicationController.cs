using Core.Feature.ApplicationDriver.Command.Models;
using Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusBookingSystem.Controllers
{
    public class DriverApplicationController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> AddApplication([FromForm] AddApplicationDriverCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }


        [HttpPut]
        public async Task<IActionResult> ApprovedApplication([FromQuery] ApproveApplicationDriverCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> RejectedApplication([FromQuery] RejectedApplicationDriverCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }


    }
}
