using Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Specifications.BookingSpecification
{
    public class BookingWithBookingSeatsSpecification : BaseSpecificaiton<Booking>
    {
        public BookingWithBookingSeatsSpecification(int id ) : base(c=>c.Id==id)
        {
            AddIncludeWith(c => c.Include(c => c.Trip));
            AddIncludeWith(c => c.Include(c => c.BookingSeats).ThenInclude(c => c.Seats));
            AddInclude(c => c.Payment);
        }
    }
}
