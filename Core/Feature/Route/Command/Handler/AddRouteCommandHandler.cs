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
        IRequestHandler<CreateRouteCommand, Response<string>>
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
    }
}
