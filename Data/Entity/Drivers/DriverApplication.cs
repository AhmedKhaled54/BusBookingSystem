using Data.Enums;
using Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity.Drivers
{
    public  class DriverApplication:BaseDriver
    {
        public int ExperienceYears  { get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.Now;
        public DateTime ReviewAt { get; set; }
        //status 
        public ApplicationDriverStatus Status { get; set; } = ApplicationDriverStatus.pending;
        public string? AdminComment { get; set; }
    }
}
