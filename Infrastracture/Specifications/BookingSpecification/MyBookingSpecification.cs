using Data.Entity;
using Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Specifications.BookingSpecification
{
    public class MyBookingSpecification : BaseSpecificaiton<Booking>
    {
        public MyBookingSpecification(int userid , BookingStatus? status) :
            base(c=>(c.UserId==userid)&&(!status.HasValue || c.Status == status.Value))
        {
            AddIncludeWith(c => c.Include(c => c.Trip).ThenInclude(c => c.Routes.StartStation));
            AddIncludeWith(c => c.Include(c => c.Trip).ThenInclude(c => c.Routes.EndStation));
            AddIncludeWith(c => c.Include(c => c.Trip).ThenInclude(c => c.Bus).ThenInclude(c=>c.Seats));
            AddInclude(c => c.Payment);
            AddInclude(c=>c.BookingSeats);
        }
    }
}
