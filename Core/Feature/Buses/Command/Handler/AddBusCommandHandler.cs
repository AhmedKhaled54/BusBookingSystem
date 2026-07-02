using AutoMapper;
using Core.Base;
using Core.Feature.Buses.Command.Models;
using Data.Entity;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.Services.BusService;
using Services.Services.FileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Buses.Command.Handler
{
    public class AddBusCommandHandler : ResponseHandler,
        IRequestHandler<AddBusCommand, Response<string>>,
        IRequestHandler<DeleteBusCommand, Response<string>>,
        IRequestHandler<DeleteSoftBusCommand, Response<string>>,
        IRequestHandler<RestoreBusCommand, Response<string>>,
        IRequestHandler<ChangeBusStatusCommand, Response<string>>
    {
        #region Feild
        private readonly IBusServices _services;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<User> _userManager;

        #endregion
        #region Ctor 
        public AddBusCommandHandler(IBusServices services, IMapper mapper, IFileService fileService, IHttpContextAccessor http, UserManager<User> userManager)
        {
            _services = services;
            _mapper = mapper;
            _fileService = fileService;
            _http = http;
            _userManager = userManager;
        }
        #endregion
        public async Task<Response<string>> Handle(AddBusCommand request, CancellationToken cancellationToken)
        {
            var bus = _mapper.Map<Bus>(request);

            if (await _services.PlateNumberExsit(request.PlateNumber))
                return BadRequest<string>("PlateNumber Already Exsit ");

            if (request.Capacity <= 0)
                return BadRequest<string>("Invalid Capacity!");

            await _services.CreateBus(bus);
            if (request != null)
            {
                var url = await _fileService.Upload(request.Image, "Buses/Images");
                bus.Image = url;
            }
            return Success("Create Bus Successfuly ");



        }

        public async Task<Response<string>> Handle(DeleteBusCommand request, CancellationToken cancellationToken)
        {
            var bus = await _services.GetBusById(request.Id);
            if (bus == null)
                return NotFound<string>("bus Not Found!");

            if (bus.Image != null)
               _fileService.Remove(bus.Image);

            _services.DeleteBus(bus);
            return Success("Remove Bus Successfuly");
        }

        public async  Task<Response<string>> Handle(DeleteSoftBusCommand request, CancellationToken cancellationToken)
        {
            var UserId = GetUserId();
            var bus = await _services.GetBusById(request.Id);
            if (bus == null)
                return NotFound<string>("bus Not Found!");
            
            _services.SoftDeleteBus(bus,UserId);
            return Success("Bus Is Deleted ");

        }

        public async Task<Response<string>> Handle(RestoreBusCommand request, CancellationToken cancellationToken)
        {
            var bus =await _services.GetDeletedBusById(request.Id);
            if (bus == null)
                return NotFound<string>("Bus Not Found !");
            bus.DeletedBy = null;
            bus.DeletedAt = null;
            bus.IsDeleted = false;
            return Success("Bus Restored Successfuly");
        }

        public async Task<Response<string>> Handle(ChangeBusStatusCommand request, CancellationToken cancellationToken)
        {
            var bus = await _services.GetBusById(request.Id);
            if (bus == null)
                return NotFound<string>("Not Found Bus");
            bus.status = request.status;
            return Success("Bus Status Updated Successfuly ");
        }

        private int GetUserId()
        {
            ClaimsPrincipal claim = _http.HttpContext.User;
            var user =int.Parse(_userManager.GetUserId(claim));
            return user;
        }
    }
}
