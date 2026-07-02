using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Specifications
{
    public  interface ISpecification<T> where T : class
    {
        Expression<Func<T,bool>> Creiteria { get; }
        List<Expression<Func<T,object>>>Includes { get; }
        List<Func<IQueryable<T>,IQueryable<T>>>IncludeWithThen { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDesc { get; }
        

    }
}
