using Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Specifications.TripsSepcification
{
    public class TripWithBus_Driver_RoutesSpecification : BaseSpecificaiton<Trip>
    {
        public TripWithBus_Driver_RoutesSpecification(string?Search= null ) 
            : base(c=>
            (string.IsNullOrEmpty(Search)||
            c.Routes.StartStation.Name.Contains(Search)|| 
            c.Routes.EndStation.Name.Contains(Search))
            )
        {
            AddInclude(c=>c.Bus);
            AddIncludeWith(c=>c.Include(c=>c.Routes).ThenInclude(c=>c.StartStation));
            AddIncludeWith(c=>c.Include(c=>c.Routes).ThenInclude(c=>c.EndStation));
            AddInclude(c => c.Bookings);

        }

        public TripWithBus_Driver_RoutesSpecification(int id ):base (c=>c.Id==id)
        {
            AddInclude(c => c.Bus);
            AddIncludeWith(c => c.Include(c => c.Routes).ThenInclude(c => c.StartStation));
            AddIncludeWith(c => c.Include(c => c.Routes).ThenInclude(c => c.EndStation));
            AddInclude(c => c.Bookings);
        }
    }
}
