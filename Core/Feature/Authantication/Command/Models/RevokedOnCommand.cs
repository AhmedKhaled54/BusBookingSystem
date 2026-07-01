using Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.Command.Models
{
    public  class RevokedOnCommand:IRequest<Response<string>>
    {
        public RevokedOnCommand(string token)
        {
            Token = token;
        }

        public string Token { get; set; }

    }
}
