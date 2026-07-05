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
    public  class GetDeletedRouteQuery:IRequest<Response<IEnumerable<GetDeletedRoutesQueryResult>>>
    {
    }
}
