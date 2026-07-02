using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.ApplicationDriver.Query.Result
{
    public  class GetDeletedBusesQueryResult
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string PlateNumber { get; set; }
        public string Image { get; set; }
        public int Capacity { get; set; }
        public BusStatus status { get; set; }
        public string Driver { get; set; }
        public string Owner { get; set; }
        public string DeletedName { get; set; }
        public DateTime? DeletedAt { get; set; }


    }
}
