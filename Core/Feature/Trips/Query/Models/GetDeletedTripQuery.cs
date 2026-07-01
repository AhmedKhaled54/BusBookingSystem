using Core.Base;
using Core.Feature.Trips.Query.Results;
using Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Trips.Query.Models
{
    public  class GetDeletedTripQuery:IRequest<Response<Pagination<GetDeletedTripQueryResult>>>
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
    }
}
