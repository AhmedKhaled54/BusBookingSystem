using Data.Entity;
using Infrastracture.Abstract;
using Infrastracture.Specifications.RouteSpecification;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
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

        public async Task CreateRoute(Routes route)
        => await _UOW.Repository<Routes>().AddAsync(route);

        public async Task<IEnumerable<Routes>> GetAllRoutes()
        {
            var specs = new RoutesSpecification();
            var result = new RoutesWithStationSpecification(specs);
            var data = await _UOW.Repository<Routes>().GetEntityWithSpecification(result).ToListAsync();
            return data;
        }


        public async Task<Routes> GetRouteById(int id)
        {
            var specs = new RoutesWithStationSpecification(id);
            var data = await _UOW.Repository<Routes>().GetEntityByIdSepecification(specs);
            return data;
        }
    }
}
