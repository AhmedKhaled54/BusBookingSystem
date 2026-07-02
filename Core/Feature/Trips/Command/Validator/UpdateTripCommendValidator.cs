using Core.Feature.Trips.Command.Models;
using FluentValidation;
using Microsoft.AspNetCore.Rewrite;
using Services.Services.TripsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Trips.Command.Validator
{
    public class UpdateTripCommendValidator : AbstractValidator<UpdateTripCommand>
    {
        private readonly ITripeServices _services;
        public UpdateTripCommendValidator(ITripeServices services)
        {
            _services = services;

            RuleFor(c => c.BusId)
                .NotEmpty()
                .GreaterThan(0).
                WithMessage("Bus Id is Required");
            
            RuleFor(c => c.DriverId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Driver Id is Required");
           
            RuleFor(c => c.DepartualTime).NotEmpty().WithMessage("Departual Time is Required");
            RuleFor(c=>c.DepartualTime).GreaterThan(DateTime.Now).WithMessage("Departual Time Must Be Greater Than Current Time");
            RuleFor(c=>c.DepartualTime).LessThan(c=>c.ArrivalTime).WithMessage("Departual Time Must Be Less Than Arrival Time");

            RuleFor(c => c.ArrivalTime).NotEmpty().WithMessage("Arrival Time is Required");
            RuleFor(c=>c.Price).NotEmpty().GreaterThan(0).WithMessage("Price Must Be Greater Than 0");
            RuleFor(c => c.Status).IsInEnum().WithMessage("Invalid Trip Status");

            Apply();
        }

        public void Apply()
        {
            RuleFor(c=>c.Id).MustAsync(async (id, ct) =>
            await _services.IsTripExsit(id)).WithMessage("No Trip With This Id");


            RuleFor(c => c).MustAsync(async (command, cancellation) =>
            {
                var driverConflict = await _services.DriverTripConflict(command.DriverId, command.DepartualTime, command.ArrivalTime, command.Id);
                var busConflict = await _services.BusTripConflict(command.BusId, command.DepartualTime, command.ArrivalTime, command.Id);
                return driverConflict && busConflict;
            }).WithMessage("Driver Or Bus Has Another Trip During This Time");
        }
    }
}
