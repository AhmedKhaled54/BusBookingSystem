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
    public  class LoginCommand:IRequest<Response<AuthResult>>
    {
        public string Email {  get; set; }
        public string Password  { get; set; }
    }
}
