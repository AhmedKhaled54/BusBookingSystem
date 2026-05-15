using Data.Enums;
using Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Booking
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int UserId { get; set; }
        public int BookingNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending; 

        public User User { get; set; }
        public Trip Trip { get; set; }
        public ICollection<BookingSeats> BookingSeats { get; set; }
        public Payment? Payment { get; set; }
        public Ticket Ticket { get; set; }
    }
}
