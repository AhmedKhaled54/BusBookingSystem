using Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Route.Command.Models
{
    public  class SoftDelteRouteCommand:IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
    }
}
