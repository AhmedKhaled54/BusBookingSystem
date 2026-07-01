using AutoMapper;
using Core.Base;
using Core.Feature.Stations.Query.Models;
using Core.Feature.Stations.Query.Result;
using Data.Identity;
using MailKit.Net.Imap;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Services.CachingServices.UserCaching;
using Services.Services.StationsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Stations.Query.Handler
{
    public class AddStationQueryHandler : ResponseHandler,
        IRequestHandler<GetAllStationQuery, Response<IEnumerable<GetAllStationQueryResult>>>,
        IRequestHandler<GetStationByIdQuery, Response<GetAllStationQueryResult>>,
        IRequestHandler<GetDeleteSataionQuery, Response<IEnumerable<GetDeleteStationQueryResult>>>
    {
        #region Feild 
        private readonly IStaitonServices _services;
        private readonly IMapper _Mapper;
        private readonly ICachUser _cachUser;
        private readonly UserManager<User>_userManager;

        #endregion
        #region Ctor 
        public AddStationQueryHandler(IStaitonServices services, IMapper mapper, ICachUser cachUser, UserManager<User> userManager)
        {
            _services = services;
            _Mapper = mapper;
            _cachUser = cachUser;
            _userManager = userManager;
        }
        #endregion
        public async Task<Response<IEnumerable<GetAllStationQueryResult>>> Handle(GetAllStationQuery request, CancellationToken cancellationToken)
        {
            var data = await _services.GetStations();
            var result = _Mapper.Map<IEnumerable<GetAllStationQueryResult>>(data);
            return Success(result);
        }

        public async Task<Response<GetAllStationQueryResult>> Handle(GetStationByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _services.GetStationsById(request.Id);
            if (data == null)
                return NotFound<GetAllStationQueryResult>("Station Not Found !");
            var result = _Mapper.Map<GetAllStationQueryResult>(data);
            return Success(result);
        }

        public async Task<Response<IEnumerable<GetDeleteStationQueryResult>>> Handle(GetDeleteSataionQuery request, CancellationToken cancellationToken)
        {
            var data = await _services.GetDeletedStation();
            var users = await _userManager.Users.ToListAsync();
            _cachUser.LoadUsers(users);
            var result =_Mapper.Map<IEnumerable<GetDeleteStationQueryResult>>(data);
            return Success(result);
        }
    }
}
