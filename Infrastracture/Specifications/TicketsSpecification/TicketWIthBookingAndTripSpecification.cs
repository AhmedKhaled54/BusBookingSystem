using Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Specifications.TicketsSpecification
{
    public class TicketWIthBookingAndTripSpecification : BaseSpecificaiton<Ticket>
    {
        public TicketWIthBookingAndTripSpecification(int bookingId ) : base(c => c.BookingId == bookingId)
      
        {
            AddIncludeWith(c => c.Include(c => c.Booking.Trip));
            AddIncludeWith(c => c.Include(c => c.Booking.User));
            AddIncludeWith(c => c.Include(c => c.Booking.BookingSeats).ThenInclude(c => c.Seats));
            AddIncludeWith(x => x.Include(t => t.Booking)
                .ThenInclude(b => b.Trip)
                    .ThenInclude(t => t.Routes)
                        .ThenInclude(r => r.StartStation));
            AddIncludeWith(x => x.Include(t => t.Booking)
            .ThenInclude(b => b.Trip)
            .ThenInclude(t => t.Routes)
            .ThenInclude(r => r.EndStation));
        }

    }
}
