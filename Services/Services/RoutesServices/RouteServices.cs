using Infrastracture.Abstract;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.RoutesServices
{
    public class RouteServices : IRouteServices
    {
        private readonly IUnitOfWork _UOW;

        public RouteServices(IUnitOfWork uOW)
        {
            _UOW = uOW;
        }

        public async Task CreateRoute(Route route)
        =>await _UOW.Repository<Route>().AddAsync(route);

        public async Task<IEnumerable<Route>> GetAllRoutes()
            =>await _UOW.Repository<Route>().GetAllAsync();

        public async Task<Route> GetRouteById(int id)
            =>await _UOW.Repository<Route>().GetByIdAsync(id);
    }
}
