using AutoMapper;
using Core.Base;
using Core.Feature.Trips.Query.Models;
using Core.Feature.Trips.Query.Results;
using Core.Wrappers;
using Data.Entity;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Services.Services.CachingServices.UserCaching;
using Services.Services.TripsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Trips.Query.Handler
{
    public class AddTripQueryHandler : ResponseHandler,
        IRequestHandler<GetAllTripsQuery, Response<Pagination<_GetTripQueryResult>>>,
        IRequestHandler<GetTripByIdQuery, Response<_GetTripQueryResult>>,
        IRequestHandler<GetDeletedTripQuery, Response<Pagination<GetDeletedTripQueryResult>>>
    {
        #region Feild 
        private readonly IMapper _mapper;
        private readonly ITripeServices _services;
        private readonly UserManager<User> _userManager;
        private readonly ICachUser _CachUser;
        #endregion
        #region Ctor 
        public AddTripQueryHandler(IMapper mapper, ITripeServices services, ICachUser cachUser, UserManager<User> userManager)
        {
            _mapper = mapper;
            _services = services;
            _CachUser = cachUser;
            _userManager = userManager;
        }
        #endregion

        public async Task<Response<Pagination<_GetTripQueryResult>>> Handle(GetAllTripsQuery request, CancellationToken cancellationToken)
        {
            var trip = _services.GetTrips(request.Search);
            var result = await trip.ToPaginationListAsync<Trip, _GetTripQueryResult>
                (request.PageSize, request.PageNumber, _mapper);
            return Success(result);
        }

        public async Task<Response<_GetTripQueryResult>> Handle(GetTripByIdQuery request, CancellationToken cancellationToken)
        {
            var trip = await _services.GetTripById(request.Id);
            if (trip == null)
                return BadRequest<_GetTripQueryResult>("No Trip With This Id");
            var result = _mapper.Map<_GetTripQueryResult>(trip);
            return Success(result);
        }

        public async  Task<Response<Pagination<GetDeletedTripQueryResult>>> Handle(GetDeletedTripQuery request, CancellationToken cancellationToken)
        {
            var users =_userManager.Users.ToList();
            _CachUser.LoadUsers(users);
            var trip = _services.GetTripDeleted();
            if (trip == null)
                return NotFound<Pagination<GetDeletedTripQueryResult>>("No Deleted Trip");

            var result =await  trip.ToPaginationListAsync<Trip, GetDeletedTripQueryResult>
                (request.PageSize,request.PageIndex,_mapper);
            return Success(result);
        }
    }
}
