using AutoMapper;
using Core.Base;
using Core.Feature.Route.Query.Models;
using Core.Feature.Route.Query.Results;
using MediatR;
using Pipelines.Sockets.Unofficial;
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
        IRequestHandler<GetRouteByIdQuery, Response<GetRoutesQueryResult>>
    {
        #region Feild
        private readonly IRouteServices _services;
        private readonly IMapper _mapper;

        #endregion
        #region Ctor 
        public AddRouteQueryHandler(IRouteServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
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
    }
}
