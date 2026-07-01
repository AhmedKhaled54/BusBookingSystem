using Org.BouncyCastle.Bcpg.Sig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Trips.Query.Results
{
    public  class _GetTripQueryResult
    {
        public int Id { get; set; }
        public DateTime DepartualTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public string BusModel { get; set; }
        public string BusImage { get; set; }
        public string FromStation { get; set; }
        public string ToStation { get; set; }
        public int Capacity  { get; set; }
        public int RemainingSeats { get; set; }

    }
}
