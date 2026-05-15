using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public  class BusLocation
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal SpeedKph { get; set; }
        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
        public Trip Trip { get; set; } = default!;
    }
}
