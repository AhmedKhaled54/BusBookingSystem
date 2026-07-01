using Data.Helper;
using Data.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Authantication
{
    public  interface IAuthanticationServices
    {
        Task<AuthResult> GetToken(User user);
        Task<ResponseDto> Register(User user,string password);
        Task<AuthResult> RefreshToken (string token );
        Task<bool> RevokedOn(string token );
        Task<ResponseDto> ConfirmEmail (int? userid ,string? code );
        Task<bool> IsExsit(string email);
    }   
}
