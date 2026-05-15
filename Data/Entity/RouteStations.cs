using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public  class RouteStations
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int StationId { get; set; }
        public int Order { get; set; }
        public TimeSpan ArrivalOffset { get; set; }
        //
        public Routes Route { get; set; }
        public Stations Station { get; set; }

    }
}
