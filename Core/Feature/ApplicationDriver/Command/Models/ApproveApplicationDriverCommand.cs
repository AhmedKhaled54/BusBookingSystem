using Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.ApplicationDriver.Command.Models
{
    public  class ApproveApplicationDriverCommand:IRequest<Response<string>>
    {
        public int AppId { get; set; }
        public string? AdminComment {  get; set; }

    }
}
