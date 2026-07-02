using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public  class Seats:BaseEntity
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        public int Number { get; set; }
        public bool IsAvailable { get; set; }

        public ICollection<BookingSeats>  BookingSeats { get; set; }

    }
}
