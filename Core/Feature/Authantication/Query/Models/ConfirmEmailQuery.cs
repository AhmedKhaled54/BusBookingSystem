using Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.Query.Models
{
    public  class ConfirmEmailQuery:IRequest<Response<string>>
    {
        public int UserId {  get; set; }
        public string Code {  get; set; }
    }
}
