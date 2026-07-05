using Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Route.Command.Models
{
    public  class RestoreRouteCommand: IRequest<Response<string>>
    {
        public int Id { get; set; }

        public RestoreRouteCommand(int id)
        {
            Id = id;
        }
    }
}
