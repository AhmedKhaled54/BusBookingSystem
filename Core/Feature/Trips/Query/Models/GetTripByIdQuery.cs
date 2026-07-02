using Core.Base;
using Core.Feature.Trips.Query.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Trips.Query.Models
{
    public  class GetTripByIdQuery:IRequest<Response<_GetTripQueryResult>>
    {
        public GetTripByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
