using Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.Command.Models
{
    public  class ResetPasswordCommand:IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string OTpCode { get; set; }
        public string NewPassword {  get; set; }

    }
}
