using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Specifications.BusSpecification
{
    public class BusWithUserAndDriverSpecification : BaseSpecificaiton<Bus>
    {
        public BusWithUserAndDriverSpecification(BusSpecification specs  ) :
            base
            (b=>
            (b.OwnerId==specs.OwnerId||!specs.OwnerId.HasValue)&&
            (b.DriverId==specs.DriverId||!specs.DriverId.HasValue)&&
            (string.IsNullOrEmpty(specs.Search)||b.Model.Contains(specs.Search))
            )
        {
            AddIncludeWith(c => c.Include(c => c.Driver).ThenInclude(c => c.User));
        }


        public BusWithUserAndDriverSpecification(int id ):base (c=>c.Id==id)
        {
            AddIncludeWith(c => c.Include(c => c.Driver).ThenInclude(c => c.User));

        }
    }
}
