using AutoMapper;
using Core.Base;
using Core.Feature.ApplicationDriver.Query.Models;
using Core.Feature.ApplicationDriver.Query.Result;
using Core.Wrappers;
using Data.Entity;
using Data.Identity;
using Infrastracture.Specifications.BusSpecification;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Services.BusService;
using Services.Services.CachingServices.UserCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.ApplicationDriver.Query.Handler
{
    public class AddBusQueryHandler : ResponseHandler,
       IRequestHandler<GetAllBusesQuery, Response<Pagination<GetAllBusesQueryResult>>>,
        IRequestHandler<GetDeletedBusesQuery, Response<Pagination<GetDeletedBusesQueryResult>>>
    {

        #region Feild 
        private readonly IBusServices _services;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly ICachUser _cachUser;
        #endregion
        #region Ctor 
        public AddBusQueryHandler(IBusServices services, IMapper mapper, IConfiguration config, UserManager<User> userManager, ICachUser cachUser)
        {
            _services = services;
            _mapper = mapper;
            _config = config;
            _userManager = userManager;
            _cachUser = cachUser;
        }
        #endregion
        public async Task<Response<Pagination<GetAllBusesQueryResult>>> Handle(GetAllBusesQuery request, CancellationToken cancellationToken)
        {

            var specs = new BusSpecification
            {
                Search = request.Search
            };

            var services = _services.GetAllBuses(specs);
            var result = await services.ToPaginationListAsync<Bus, GetAllBusesQueryResult>
                (request.PageSize,request.PageIndex, _mapper);

            return Success(result);

        }

        public async Task<Response<Pagination<GetDeletedBusesQueryResult>>> Handle(GetDeletedBusesQuery request, CancellationToken cancellationToken)
        {

            #region before 
            //var query = from bus in buses
            //            join user in _userManager.Users
            //            on bus.DeletedBy equals user.Id into users
            //            from deleteduser in users.DefaultIfEmpty()
            //            select (new GetDeletedBusesQueryResult
            //            {
            //                Id = bus.Id,
            //                Model = bus.Model,
            //                PlateNumber = bus.PlateNumber,
            //                Capacity = bus.Capacity,
            //                DeletedAt = bus.DeletedAt,
            //                DeletedName = deleteduser.FullName,
            //                Driver = bus.Driver.FullName,
            //                Owner=bus.Owner.FullName
            //            });



            //var result =await query.ToPaginationListAsync(request.PageSize,request.PageIndex);
            #endregion
            var buses = _services.GetBusesDeleted();
            var users = await _userManager.Users.ToListAsync();
            _cachUser.LoadUsers(users);

            var result = await buses.ToPaginationListAsync<Bus,GetDeletedBusesQueryResult>
                (request.PageSize,request.PageIndex,_mapper);
            return Success(result);

        }
    }
}
