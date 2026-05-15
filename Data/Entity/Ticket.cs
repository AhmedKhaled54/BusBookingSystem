using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public  class Ticket
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string QRCode { get; set; }
        public TicketStatus Status { get; set; } = TicketStatus.Active;
        public DateTime IssuedAt { get; set; }= DateTime.Now;
        public int BookingId { get; set; }
        public Booking Booking  { get; set; }
    }
}
