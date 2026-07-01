using AutoMapper;
using Core.Base;
using Core.Feature.Trips.Command.Models;
using Data.Entity;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Pipelines.Sockets.Unofficial;
using Services.Services.TripsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Trips.Command.Handler
{
    public class AddTripCommandHandler : ResponseHandler,
        IRequestHandler<CreateTripCommand, Response<string>>,
        IRequestHandler<UpdateTripCommand, Response<string>>,
        IRequestHandler<ChangeTripStatusCommand, Response<string>>,
        IRequestHandler<DeleteSoftTripCommand, Response<string>>,
        IRequestHandler<RestoreTripCommand, Response<string>>
    {
        #region Feild
        private readonly ITripeServices _services;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _usermanger;
        private readonly IHttpContextAccessor _http;
        #endregion
        #region Ctor 
        public AddTripCommandHandler(ITripeServices services, IMapper mapper, UserManager<User> usermanger, IHttpContextAccessor http)
        {
            _services = services;
            _mapper = mapper;
            _usermanger = usermanger;
            _http = http;
        }
        #endregion
        public async Task<Response<string>> Handle(CreateTripCommand request, CancellationToken cancellationToken)
        {
            var trip = _mapper.Map<Trip>(request);

            if (!await _services.DriverTripConflict(request.DriverId, request.DepartualTime, request.ArrivalTime))
                return BadRequest<string>("Driver Already assigned To Another Trip ");

            if (!await _services.BusTripConflict(request.BusId, request.DepartualTime, request.ArrivalTime))
                return BadRequest<string>("Bus Already assigned To Another Trip ");

            await _services.CreateTrip(trip);
            return Success("Added Successfuly ");

        }

        public async Task<Response<string>> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
        {

            var trip = await _services.GetTripById(request.Id);
            if (trip is null)
                return NotFound<string>("Trip Not Found");

            if (!await _services.DriverTripConflict(request.DriverId, request.DepartualTime, request.ArrivalTime, request.Id))
                return BadRequest<string>("Driver Already assigned To Another Trip ");

            if (!await _services.BusTripConflict(request.BusId, request.DepartualTime, request.ArrivalTime, request.Id))
                return BadRequest<string>("Bus Already assigned To Another Trip ");

            var result = _mapper.Map(request, trip);
            _services.UpdateTrip(result);

            return Success("Updated Successfuly ");
        }

        public async Task<Response<string>> Handle(ChangeTripStatusCommand request, CancellationToken cancellationToken)
        {
            var trip = await _services.GetTripById(request.TripId);
            if (trip is null)
                return NotFound<string>("Trip Not Found");
            _services.ChangeTripStatus(trip, request.Status);
            return Success("Status Updated Successfuly ");
        }

        public async Task<Response<string>> Handle(DeleteSoftTripCommand request, CancellationToken cancellationToken)
        {
            var userid = GetUserId();

            var trip = await _services.GetTripById(request.Id);
            if (trip is null)
                return NotFound<string>("Trip Not Found");

            _services.SoftDeleteTrip(trip, userid);
            return Success("Deleted Successfuly ");
        }

        public async Task<Response<string>> Handle(RestoreTripCommand request, CancellationToken cancellationToken)
        {
            var trip = await _services.GetDeletedTripById(request.Id);
            if (trip is null)
                return NotFound<string>("Trip Not Found");
            trip.IsDeleted = false;
            trip.DeletedBy = null;
            trip.DeletedAt = null;

            return Success("Restored Successfuly ");
        }

        private int GetUserId()
        {
            ClaimsPrincipal claim = _http.HttpContext.User;
            var userId = int.Parse(_usermanger.GetUserId(claim));
            return userId;
        }
    }
}
