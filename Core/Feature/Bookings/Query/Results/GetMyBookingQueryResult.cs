using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Booking.Query.Results
{
    public class GetMyBookingQueryResult
    {
        public int BookingNumber  { get; set; }
        public string StartStation { get; set; }
        public string EndStation { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int SeatsCount { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        
    }
}
