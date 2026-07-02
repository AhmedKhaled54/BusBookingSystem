using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Specifications
{
    public  class EvaluationSpecification<T> where T : class
    {
        public static IQueryable<T> GetQuery (IQueryable<T> Query,ISpecification<T> specification)
        {
            var query = Query.AsQueryable();

            
            if (specification.Creiteria!=null)
                query=query.Where(specification.Creiteria);

            if (specification.OrderBy!=null)
                query = query.OrderBy(specification.OrderBy);

            if (specification.OrderByDesc!=null)
                query=query.OrderByDescending(specification.OrderByDesc);

            query=specification.Includes.Aggregate( query,(current,include)=>current.Include(include));


            foreach (var includes in specification.IncludeWithThen)
                query = includes(query);


            return query;

        }
    }
}
