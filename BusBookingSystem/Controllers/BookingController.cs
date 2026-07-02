using Core.Feature.Booking.Command.Models;
using Core.Feature.Booking.Query.Models;
using Core.Feature.Bookings.Command.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Icao;

namespace BusBookingSystem.Controllers
{

    public class BookingController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromQuery] CreateBookingCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);

        }


        [HttpGet]
        public async Task<IActionResult> GetMyBookings([FromQuery] GetMyBookingQuery query)
        {
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Cancel([FromQuery] CancelBooikingCommand cmd)
        {
            var result =await _Mediator.Send(cmd);  
            return NewResult(result);
        }
    }
}
