using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Specifications.RouteSpecification
{
    public class RoutesWithStationSpecification : BaseSpecificaiton<Routes>
    {
        public RoutesWithStationSpecification(RoutesSpecification specs): base
            (c =>
            (!specs.StartStationId.HasValue || c.StartStationId == specs.StartStationId) &&
            (!specs.EndStationId.HasValue || c.EndStationId == specs.EndStationId)
            )
        {
            AddInclude(c => c.StartStation);
            AddInclude(c => c.EndStation);
        }


        public RoutesWithStationSpecification(int id) : base(c => c.Id == id)
        {
            AddInclude(c => c.StartStation);
            AddInclude(c => c.EndStation);
        }
    }
}
