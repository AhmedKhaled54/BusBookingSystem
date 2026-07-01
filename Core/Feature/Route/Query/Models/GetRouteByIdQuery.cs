using Core.Base;
using Core.Feature.Route.Query.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Route.Query.Models
{
    public  class GetRouteByIdQuery:IRequest<Response<GetRoutesQueryResult>>
    {
        public int Id { get; set; }

        public GetRouteByIdQuery(int id)
        {
            Id = id;
        }
    }
}
