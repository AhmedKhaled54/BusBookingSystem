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
    public  class GetAllTripsQuery:IRequest<Response<Pagination<_GetTripQueryResult>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
    }
}
