using AutoMapper;
using Core.Base;
using Core.Feature.Route.Command.Models;
using Data.Entity;
using MediatR;
using Services.Services.RoutesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Route.Command.Handler
{
    public class AddRouteCommandHandler : ResponseHandler,
        IRequestHandler<CreateRouteCommand, Response<string>>,
        IRequestHandler<UpdateRouteCommand, Response<string>>,
        IRequestHandler<SoftDelteRouteCommand, Response<string>>,
        IRequestHandler<RestoreRouteCommand, Response<string>>
    {

        #region Feild 
        private readonly IRouteServices _services;
        private readonly IMapper _mapper;

        #endregion
        #region Ctor 
        public AddRouteCommandHandler(IRouteServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<string>> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
        {
            var route = _mapper.Map<Routes>(request);
            await _services.CreateRoute(route);
            return Success("Create Route Succssfuly ");
        }

        public async Task<Response<string>> Handle(UpdateRouteCommand request, CancellationToken cancellationToken)
        {
            var route =await _services.GetRouteById(request.Id);
            if (route == null)
                return NotFound<string>("Route Not Found");
            var reult = _mapper.Map(request, route);
             _services.UpdateRoute(reult);
            return Success("Update Route Succssfuly ");
        }

        public async Task<Response<string>> Handle(SoftDelteRouteCommand request, CancellationToken cancellationToken)
        {
            var route =await  _services.GetRouteById(request.Id);
            if (route == null)
                return NotFound <string>("Route Not Found");
            
            _services.SoftDeleteRoute(route, request.UserId);
            return Success("Delete Route Succssfuly ");

        }

        public async Task<Response<string>> Handle(RestoreRouteCommand request, CancellationToken cancellationToken)
        {
            var route =await _services.GetDeletedRouteById(request.Id);
            if (route == null)
                return NotFound<string>("Route Not Found");
            route.IsDeleted= false;
            route.DeletedBy = null;
            route.DeletedAt = null;
           
            return Success("Restore Route Succssfuly ");
        }
    }
}
