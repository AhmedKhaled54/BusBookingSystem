using Data.Entity.Drivers;
using Data.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Trip
    {
        public int Id { get; set; }
        public DateTime DepartualTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        //status 
        public TripeStatus Status { get; set; } = TripeStatus.Schadule;
         
        public int DriverId { get; set; }
        public int BusId { get; set; }
        public int RouteId { get; set; }




        //navigation 
        public Driver Driver { get; set; }
        public Bus Bus { get; set; }
        public Routes Routes { get; set; }
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<BusLocation> BusLocations { get; set; } 
    }
}
