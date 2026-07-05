using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Specifications.RouteSpecification
{
    public class DeletedRoutesWithStationSpecification : BaseSpecificaiton<Routes>
    {
        public DeletedRoutesWithStationSpecification(Expression<Func<Routes, bool>> creteria) : base(creteria)
        {
            AddInclude(c => c.StartStation);
            AddInclude(c => c.EndStation);  
        }
    }
}
