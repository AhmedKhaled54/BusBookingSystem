using Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Stations.Command.Models
{
    public  class UpdateStationCommand:IRequest<Response<string>>
    {
        public int Id   { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

    }
}
