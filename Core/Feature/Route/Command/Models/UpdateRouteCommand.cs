using Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Route.Command.Models
{
    public  class UpdateRouteCommand: IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Distance { get; set; }
        public int StartStatoin { get; set; }
        public int EndStatoin { get; set; }
    }
}
