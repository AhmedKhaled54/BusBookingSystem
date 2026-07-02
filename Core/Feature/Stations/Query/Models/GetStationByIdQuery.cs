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
    public  class GetStationByIdQuery:IRequest<Response<GetAllStationQueryResult>>
    {
        public int Id { get; set; }

        public GetStationByIdQuery(int id)
        {
            Id = id;
        }
    }
}
