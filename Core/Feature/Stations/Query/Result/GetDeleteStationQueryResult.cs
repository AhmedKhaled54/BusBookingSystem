using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Stations.Query.Result
{
    public  class GetDeleteStationQueryResult
    {
        public string Name { get; set; }
        public string City { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string DeletedName { get; set; }
        public DateTime DeletedAt { get; set; }


    }
}
