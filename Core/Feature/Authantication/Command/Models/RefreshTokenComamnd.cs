using Core.Base;
using Data.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.Command.Models
{
    public  class RefreshTokenComamnd:IRequest<Response<AuthResult>>
    {
        public string Token { get; set; }
    }
}
