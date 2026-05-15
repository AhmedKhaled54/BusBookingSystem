using Data.Enums;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public  class BookingSeats
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int SeatsId { get; set; }
        public decimal Price { get; set; }
        public SeatsType Status { get; set; } = SeatsType.Reserved;
        public Booking Booking { get; set; }
        public Seats Seats { get; set; }


    }
}
