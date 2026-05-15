using Data.Entity.Drivers;
using Data.Enums;
using Data.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Bus
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string PlateNumber { get; set; }
        public string Image { get; set; }
        public int Capacity { get; set; }
        public BusStatus status { get; set; } = BusStatus.Active;
        public int DriverId { get; set; }
        public int OwnerId { get; set; }

        public Driver Driver { get; set; }
        public User Owner { get; set; }

        public ICollection<Seats> Seats { get; set; } = new List<Seats>();
        public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}
