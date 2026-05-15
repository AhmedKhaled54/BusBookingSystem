using Data.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity.Drivers
{
    public  class Driver:BaseDriver
    {
       
        public int TotalTrips { get; set; }
        public double Rating { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection <Review> Reviews { get; set; }
        public ICollection<Bus> Buses { get; set; }


    }
}
