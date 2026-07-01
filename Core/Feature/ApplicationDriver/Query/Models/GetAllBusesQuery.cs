using Core.Base;
using Core.Feature.ApplicationDriver.Query.Result;
using Core.Wrappers;
using MediatR;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.ApplicationDriver.Query.Models
{
    public  class GetAllBusesQuery:IRequest<Response<Pagination<GetAllBusesQueryResult>>>
    {
        public string? Search { get; set; }
        public int PageSize { get; set; }
        public int PageIndex  { get; set; }
    }
}
