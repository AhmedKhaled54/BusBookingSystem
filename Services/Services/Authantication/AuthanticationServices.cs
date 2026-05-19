using Data.Enums;
using Data.Helper;
using Data.Identity;
using Infrastracture.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.IdentityModel.Tokens;
using Services.Services.EmailServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Authantication
{
    public class AuthanticationServices : IAuthanticationServices
    {
        private readonly UserManager<User> _userManager;
        private readonly JWT _jwt;
        private readonly IUnitOfWork _UOW;
        private readonly IUrlHelper _url;
        private readonly IHttpContextAccessor _http;
        private readonly IEmailService _emailService;
        public AuthanticationServices
            (
            UserManager<User> userManager,
            JWT jwt,
            IUnitOfWork uOW,
            IUrlHelper url,
            IEmailService emailService,
            IHttpContextAccessor http)
        {
            _userManager = userManager;
            _jwt = jwt;
            _UOW = uOW;
            _url = url;
            _emailService = emailService;
            _http = http;
        }


        public async Task<AuthResult> GetToken(User user)
        {
            var accesstoken = await GenerateJwtToken(user);
            var refreshtoken = GenerateRefreshToken(user.Id);
            await _UOW.Repository<RefreshToken>().AddAsync(refreshtoken);
            await _UOW.Complete();

            return new AuthResult
            {
                Token = accesstoken,
                RefreshToken = refreshtoken.Token,
                RefreshTokenExpire = refreshtoken.Expires
            };

        }

        public async Task<AuthResult> RefreshToken(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(c => c.RefreshTokens.Any(c => c.Token == token));
            if (user == null)
                return new AuthResult { Message = "Invalid Token !" };
            var RefreshToken = await _UOW.Repository<RefreshToken>().FindAsync(c => c.Token == token);
            if (!RefreshToken.IsActive)
                return new AuthResult { Message = "InActive Token !" };

            RefreshToken.RevokedOn = DateTime.Now;
            var NewRefreshToken = GenerateRefreshToken(RefreshToken.UserId);

            RefreshToken.Token = NewRefreshToken.Token;
            await _UOW.Complete();
            var accesstoken = await GenerateJwtToken(user);
            return new AuthResult
            {
                Token = accesstoken,
                RefreshToken = NewRefreshToken.Token,
                RefreshTokenExpire = NewRefreshToken.Expires
            };

        }

        public async Task<ResponseDto> Register(User user, string password)
        {
            var trans = await _UOW.TransactionAsync();
            try
            {
                if (await _userManager.FindByEmailAsync(user.Email) != null)
                    return new ResponseDto { Message = "Email  Is Exsit !" };
                var baseusername = $"{user.FirstName}{user.FirstName}".Replace(" ", "");

                if (await _userManager.FindByNameAsync(baseusername) != null)
                    return new ResponseDto { Message = "UserName Already Register!" };
                user.UserName = $"{baseusername} {Random.Shared.Next(100, 999)}";
                user.UserName = user.UserName.Replace(" ", "");
                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    var error = string.Join(",", result.Errors.Select(c => c.Description));
                    return new ResponseDto { Message = error };

                }
                await _userManager.AddToRoleAsync(user, RolesPermession.User.ToString());

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var access = _http.HttpContext.Request;
                var Url = access.Scheme + "://" + access.Host + _url.Action("ConfirmEmail", "Authantication",
                    new { UserId = user.Id, Code = code });
                var message = $"To Confirm Email  Click Link  : <a href = '{Url}'> Link Of Confirm Email</a>  ";
                await _emailService.SendEmail("ConfirmEmail", message, user.Email);
                await trans.CommitAsync();

                return new ResponseDto { Message = "Check Your Email  Please Confirmed", Success = true };


            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return new ResponseDto { Message = ex.Message, Success = false };
            }


        }

        public async Task<bool> RevokedOn(string token)
        {
           var refreshToken =await _UOW.Repository<RefreshToken>().FindAsync(c=>c.Token==token);
            if (refreshToken == null)
                return false;
            refreshToken.RevokedOn = DateTime.Now;
            await _UOW.Complete();
            return true;

        }
        public async Task<ResponseDto> ConfirmEmail(int? userid, string? code)
        {

            if (userid == null || code == null)
                return new ResponseDto { Message = "Error When Confirm Email Please Try Again !" };

            var user = await _userManager.FindByIdAsync(userid.ToString());
            var confirmemail = await _userManager.ConfirmEmailAsync(user, code);
            if (!confirmemail.Succeeded)
                return new ResponseDto { Message = "Error When Confirm Email Please Try Again !" };
            return new ResponseDto
            {
                Message = "Confirm Email Successfuly ",
                Success = true
            };


        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var Claims = await GetCalims(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var Jwt = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: Claims,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays));

            var AccessToken = new JwtSecurityTokenHandler().WriteToken(Jwt);
            return AccessToken;

        }

        private async Task<List<Claim>> GetCalims(User user)
        {

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.Email,user.Email!)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
            return claims;
        }


        private RefreshToken GenerateRefreshToken(int userid)
        {
            var random = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(random);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(random),
                CreatedAt = DateTime.Now,
                Expires = DateTime.Now.AddDays(7),
                UserId = userid
            };
        }

        public async Task<bool> IsExsit(string email)
            =>await _userManager.FindByEmailAsync(email)!=null;
    }


}

