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
    public  class GetDeletedBusesQuery:IRequest<Response<Pagination<GetDeletedBusesQueryResult>>>
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
    }
}
