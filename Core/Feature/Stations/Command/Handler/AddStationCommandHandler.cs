using AutoMapper;
using Core.Base;
using Core.Feature.Stations.Command.Models;
using Data.Entity;
using MediatR;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using Services.Services.StationsServices;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Stations.Command.Handler
{
    public class AddStationCommandHandler : ResponseHandler,
        IRequestHandler<CreateStationCommand, Response<string>>,
        IRequestHandler<DeletedSoftStationCommand, Response<string>>,
        IRequestHandler<UpdateStationCommand, Response<string>>
    {
        #region Feild 

        private readonly IStaitonServices _staitonServices;
        private readonly IMapper _Mapper;
        #endregion

        #region Ctor 
        public AddStationCommandHandler(IStaitonServices staitonServices, IMapper mapper)
        {
            _staitonServices = staitonServices;
            _Mapper = mapper;
        }
        #endregion
        public async Task<Response<string>> Handle(CreateStationCommand request, CancellationToken cancellationToken)
        {
            var station = _Mapper.Map<Data.Entity.Stations>(request);

            var exsit = await _staitonServices.IsExsit(request.Name, request.City);
            if (exsit)
                return BadRequest<string>("Station Already Exists");
            await _staitonServices.CreateStation(station);
            return Success("Create Station Successfuly !");
        }

        public async Task<Response<string>> Handle(DeletedSoftStationCommand request, CancellationToken cancellationToken)
        {
            var station = await _staitonServices.GetStationsById(request.Id);
            
            if (station==null)
                return NotFound<string>("Station Not Found! ");
            _staitonServices.SoftDeleteStation(station, request.UserId);
            return Success("Deleted Successfuly");
        }

        public async Task<Response<string>> Handle(UpdateStationCommand request, CancellationToken cancellationToken)
        {
            var station = await _staitonServices.GetStationsById(request.Id);
            if (station==null)
                return NotFound<string>("Station Not Found!");

            var result =_Mapper.Map(request,station);
            _staitonServices.Update(result);
            return Success("Update Station Successfuly ");
        }
    }
}
