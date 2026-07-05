using Data.Entity;
using Microsoft.AspNetCore.Routing;
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
        bool UpdateRoute(Routes route);
        bool SoftDeleteRoute(Routes route,int userid);
        Task<Routes> GetDeletedRouteById(int id);
        Task<IEnumerable<Routes>> GetDeletedRoutes();

    }
}
