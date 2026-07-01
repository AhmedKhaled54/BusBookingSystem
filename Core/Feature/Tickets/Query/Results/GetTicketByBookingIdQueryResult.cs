using Data.Enums;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Tickets.Query.Results
{
    public  class GetTicketByBookingIdQueryResult
    {
        public int TicketNumber {  get; set; }
        public string From {  get; set; }
        public string To { get; set; }
        public DateTime TripDate { get; set; }
        public decimal Price {  get; set; }
        public List<int> SeatNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone  { get; set; }
        public string QRCode {  get; set; }
        public TicketStatus TicketStatus { get; set; }
        public DateTime IssuedAt {  get; set; }
    }
}
