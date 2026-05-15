using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public  class Routes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double DisTance { get; set; }

        public int StartStationId { get; set; }
        public int EndStationId { get; set; }
        //navegation 
        public Stations StartStation { get; set; }
        public Stations EndStation { get; set; }

        public ICollection <Trip> Trips { get; set; }
        public ICollection <RouteStations > RouteStations { get; set; } = new List<RouteStations> ();
    }
}
