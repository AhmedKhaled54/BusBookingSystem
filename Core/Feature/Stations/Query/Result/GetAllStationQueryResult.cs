using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Stations.Query.Result
{
    public  class GetAllStationQueryResult
    {
        public string Name { get; set; }
        public string City { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

    }
}
