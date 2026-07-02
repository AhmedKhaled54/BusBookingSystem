using Data.Entity;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.RoutesServices
{
    public  interface IRouteServices
    {
        Task CreateRoute(Routes route);
        Task<IEnumerable<Routes>> GetAllRoutes();
        Task<Routes> GetRouteById (int id );
    }
}
