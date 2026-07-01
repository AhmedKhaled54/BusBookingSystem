using Core.Feature.Authantication.Command.Models;
using Core.Feature.Authantication.Query.Models;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BusBookingSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthanticationController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            SetRefreshTokenInCookie(result.Data.Token, result.Data.RefreshTokenExpire);
            return NewResult(result);
        }


        [HttpPost]
        public async Task<IActionResult> ForgetPassword([FromQuery] ForgetPasswordCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword (ResetPasswordCommand cmd)
        {
            var result =await _Mediator.Send(cmd);
            return NewResult(result);

        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword (ChangePasswordCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
        {
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenComamnd cmd)
        {
            var token = cmd.Token ?? Request.Cookies["RefreshToken"];
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokedOn()
        {
            var token = Request.Cookies["RefreshToken"];
            var cmd = new RevokedOnCommand(token);
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }
        private void SetRefreshTokenInCookie(string token, DateTime time)
        {
            var cookie = new CookieOptions
            {
                HttpOnly = true,
                Expires = time.ToLocalTime()
            };
            Response.Cookies.Append("RefreshToken", token, cookie);
        }
    }
}
