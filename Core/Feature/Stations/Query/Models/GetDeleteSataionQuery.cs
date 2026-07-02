using Core.Base;
using Core.Feature.Stations.Query.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Stations.Query.Models
{
    public  class GetDeleteSataionQuery:IRequest<Response<IEnumerable<GetDeleteStationQueryResult>>>
    {

    }
}
