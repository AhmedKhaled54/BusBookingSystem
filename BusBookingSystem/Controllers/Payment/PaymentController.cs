using Core.Feature.Paymob.Command.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusBookingSystem.Controllers.Payment
{
    [ApiController]
    [Route("api/Payment")]
    public class PaymentController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> ProceedPaymentByIntention([FromBody] ProceedPaymentByIntentionIdCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpPost("CallBack")]
        [AllowAnonymous]
        public async Task<IActionResult> CallBack([FromBody] TransactionCallBackCommand command, [FromQuery] string hmac)
        {
            command.Hmac = hmac;

            var result = await _Mediator.Send(command);
            return NewResult(result);

        }
    }
}
