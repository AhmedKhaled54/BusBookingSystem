using AutoMapper;
using AutoMapper.Execution;
using Core.Base;
using Core.Feature.Authantication.Command.Models;
using Data.Helper;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Services.Services.Authantication;
using Services.Services.CachingServices.Redis;
using Services.Services.EmailServices;
using Services.Services.OtpService;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.Command.Handler
{
    public class AuthanticationCommandHandler : ResponseHandler,
        IRequestHandler<RegisterCommand, Response<string>>,
        IRequestHandler<LoginCommand, Response<AuthResult>>,
        IRequestHandler<RefreshTokenComamnd, Response<AuthResult>>,
        IRequestHandler<RevokedOnCommand, Response<string>>,
        IRequestHandler<ForgetPasswordCommand, Response<string>>,
        IRequestHandler<ResetPasswordCommand, Response<string>>,
        IRequestHandler<ChangePasswordCommand, Response<string>>
    {
        #region Feild

        private readonly IAuthanticationServices _services;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IOtpServices _otpServices;
        private readonly ICachServices _cachServices;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _http;

        #endregion

        #region Ctor 
        public AuthanticationCommandHandler
            (
            IAuthanticationServices services,
            IMapper mapper
,
            UserManager<User> userManager,
            IOtpServices otpServices,
            ICachServices cachServices,
            IEmailService emailService,
            IHttpContextAccessor http)
        {
            _services = services;
            _mapper = mapper;
            _userManager = userManager;
            _otpServices = otpServices;
            _cachServices = cachServices;
            _emailService = emailService;
            _http = http;
        }
        #endregion
        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<User>(request);
            var result = await _services.Register(map, request.Password);
            if (!result.Success)
                return BadRequest<string>(result.Message);
            return Success(result.Message);
        }

        public async Task<Response<AuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return BadRequest<AuthResult>("Incorrect Email Or Password !");

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return BadRequest<AuthResult>("Email Not Confirmed Please Try Again !");

            var result = await _services.GetToken(user);

            return Created(result);
        }

        public async Task<Response<AuthResult>> Handle(RefreshTokenComamnd request, CancellationToken cancellationToken)
        {
            var result = await _services.RefreshToken(request.Token);
            if (result.Message != null)
                return BadRequest<AuthResult>(result.Message);
            return Success(result);
        }

        public async Task<Response<string>> Handle(RevokedOnCommand request, CancellationToken cancellationToken)
        {
            var token = await _services.RevokedOn(request.Token);
            if (!token)
                return BadRequest<string>("Invalid Token !");
            return Success("Revoked Token Successfuly !");
        }

        public async Task<Response<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            //verfy email 
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return NotFound<string>("Email Not Found !");
            var OtpCode = GenerateOtp();
            await _otpServices.SaveOtp(request.Email, OtpCode);

            var body = GenerateEmailBody(OtpCode, user.UserName);
            await _emailService.SendEmail("ForgetPassword", body, request.Email);
            return Success("Check Your Email ");



        }
        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {

            var OtpCode = await _otpServices.GetOtp(request.Email);
            if (OtpCode is null)
                return BadRequest<string>("verification Code Is Expired.Please try Again !");

            if (OtpCode.Trim().Replace("\"", "") != request.OTpCode)
                return BadRequest<string>("Invalid Code Please Try Again !");

            var email =await _userManager.FindByEmailAsync(request.Email);  

            var password =_userManager.PasswordHasher.HashPassword(email,request.NewPassword);

            email.PasswordHash = password;
            await _userManager.UpdateAsync(email);
            await _otpServices.Delete(OtpCode);
            return Success("Your password has been reset successfully. You can now log in with your new password.");

        }
        public async  Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userid = GetUserId();
            if (userid <= 0)
                return UnAuthorize<string>("UnAuthorized Login And Try Again !");

            var User =await _userManager.Users.FirstOrDefaultAsync(c=>c.Id== userid);
            var result =await _userManager.ChangePasswordAsync(User,request.CurrentPassword,request.NewPassword);
            if (!result.Succeeded)
            {
                var error = string.Join(",", result.Errors.Select(c => c.Description));
                return BadRequest<string>(error);
            }

            return Success("Change Password Successfuly");
        }



        private int GetUserId()
        {
            ClaimsPrincipal claim = _http.HttpContext.User;
            var userid=int.Parse(_userManager.GetUserId(claim));
            return userid;

        }
        private string GenerateOtp()
        {
            var otp = new Random();
            var code = string.Empty;
            for (var i = 0; i < 6; i++)
            {
                code += otp.Next(1, 10).ToString();
            }
            return code;
        }
        static string GenerateEmailBody(string resetCode, string name)
        {
            return $@"
        <html>
        <body>
            <h2>Password Reset Request</h2>
            <p>Dear {name},</p>
            <p>We received a request to reset your password. Please use the following code to reset your password:</p>
            <h3>{resetCode}</h3>
            <p>If you did not request a password reset, please ignore this email.</p>
            <p>Thank you,<br> Ahmed Khaled [Owner]</p>
        </body>
        </html>";
        }

        
    }
}
