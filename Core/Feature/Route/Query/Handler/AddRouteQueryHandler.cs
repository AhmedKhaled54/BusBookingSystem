using AutoMapper;
using Core.Base;
using Core.Feature.Route.Query.Models;
using Core.Feature.Route.Query.Results;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Pipelines.Sockets.Unofficial;
using Services.Services.CachingServices.UserCaching;
using Services.Services.RoutesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Route.Query.Handler
{
    public class AddRouteQueryHandler : ResponseHandler,
        IRequestHandler<GetAllRoutesQuery, Response<IEnumerable<GetRoutesQueryResult>>>,
        IRequestHandler<GetRouteByIdQuery, Response<GetRoutesQueryResult>>,
        IRequestHandler<GetDeletedRouteQuery, Response<IEnumerable<GetDeletedRoutesQueryResult>>>
    {
        #region Feild
        private readonly IRouteServices _services;
        private readonly IMapper _mapper;
        private readonly ICachUser _Cachuser;
        private readonly UserManager<User> _userManager;

        #endregion
        #region Ctor 
        public AddRouteQueryHandler(IRouteServices services, IMapper mapper, ICachUser cachuser, UserManager<User> userManager)
        {
            _services = services;
            _mapper = mapper;
            _Cachuser = cachuser;
            _userManager = userManager;
        }
        #endregion
        public async Task<Response<IEnumerable<GetRoutesQueryResult>>> Handle(GetAllRoutesQuery request, CancellationToken cancellationToken)
        {
            var date = await _services.GetAllRoutes();
            var result =_mapper.Map<IEnumerable<GetRoutesQueryResult>>(date);
            return Success(result);

        }

        public async Task<Response<GetRoutesQueryResult>> Handle(GetRouteByIdQuery request, CancellationToken cancellationToken)
        {
            var route =await  _services.GetRouteById(request.Id);
            var result = _mapper.Map<GetRoutesQueryResult>(route);
            return Success(result);
        }

        public async Task<Response<IEnumerable<GetDeletedRoutesQueryResult>>> Handle(GetDeletedRouteQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.ToList();
            _Cachuser.LoadUsers(users);
            var routes =await  _services.GetDeletedRoutes();
            if (routes == null)
                return NotFound<IEnumerable<GetDeletedRoutesQueryResult>>("No deleted routes found.");
            
            var result = _mapper.Map<IEnumerable<GetDeletedRoutesQueryResult>>(routes);
            return Success(result);
        }

    }
}
