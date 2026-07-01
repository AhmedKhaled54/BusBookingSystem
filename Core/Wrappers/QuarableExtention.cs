using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Wrappers
{
    public static  class QuarableExtention
    {
        public static async Task<Pagination<Tdest>> ToPaginationListAsync<Tsource,Tdest>
            (this IQueryable<Tsource> query, int pagesize, int pageindex,IMapper?mapper) 
            where Tsource : class
            where Tdest : class
        {
            if (query == null)
                throw new ArgumentNullException ("No Data !");

            pageindex=pageindex==0 ? 1: pageindex;
            pagesize=pagesize==0 ? 10 : pagesize;

            int count =await query.AsNoTracking().CountAsync();
            if (count == 0)
                return Pagination<Tdest>.Paginated(new List<Tdest> (),count,pageindex,pagesize);

            var paginated = await query.AsNoTracking().Skip((pageindex - 1) * pagesize).Take(pagesize).ToListAsync();

            var Map=mapper.Map<List<Tdest>>(paginated);
            return Pagination<Tdest>.Paginated(Map,count,pageindex,pagesize);

        }
    }
}
