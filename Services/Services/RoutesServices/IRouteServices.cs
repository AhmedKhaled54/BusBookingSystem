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
        Task CreateRoute(Route route);
        Task<IEnumerable<Route>> GetAllRoutes();
        Task<Route> GetRouteById (int id );
    }
}
