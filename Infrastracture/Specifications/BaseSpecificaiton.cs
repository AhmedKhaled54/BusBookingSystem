using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Specifications
{
    public class BaseSpecificaiton<T> : ISpecification<T> where T : class
    {
        public BaseSpecificaiton(Expression<Func<T, bool>> creteria)
        {
            Creiteria = creteria;

        }
        public Expression<Func<T, bool>> Creiteria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public List<Func<IQueryable<T>, IQueryable<T>>> IncludeWithThen { get;set; }=new List<Func<IQueryable<T>, IQueryable<T>>>();

        public Expression<Func<T, object>> OrderBy { get;private set; }

        public Expression<Func<T, object>> OrderByDesc { get;private set; }

        protected void AddOrderBy (Expression<Func<T, object>> orderby)
            =>OrderBy=orderby;
        protected void AddOrderByDesc (Expression<Func<T, object>> orderbydesc)
            =>OrderByDesc=orderbydesc;

        protected void AddInclude (Expression<Func<T, object>> include)
            =>Includes.Add(include);

        protected void AddIncludeWith(Func<IQueryable<T>,IQueryable<T>> includewith)
            =>IncludeWithThen.Add(includewith);

    }
}
