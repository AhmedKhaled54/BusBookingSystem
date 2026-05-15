using Data.Entity.Drivers;
using Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Review
    {
        public int Id { get; set; }
        public int Rate { get; private set; } = 1;
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public int? TripId { get; set; }
        public int? DriverId { get; set; }// optional 


        public User User { get; set; }
        public Trip Trip { get; set; }
        public Driver Driver { get; set; }

        public void SetRate(int rate)
        {
            if (rate < 1 || rate > 5)
                throw new ArgumentException("Rate must be between 1 and 5.");
            Rate = rate;
        }

    }
}
