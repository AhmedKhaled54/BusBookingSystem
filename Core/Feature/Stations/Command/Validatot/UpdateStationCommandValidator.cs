using Core.Feature.Stations.Command.Models;
using FluentValidation;
using Services.Services.StationsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Stations.Command.Validatot
{
    public  class UpdateStationCommandValidator:AbstractValidator<UpdateStationCommand>
    {
        private readonly IStaitonServices _services;
        public UpdateStationCommandValidator(IStaitonServices services)
        {
            _services = services;

            RuleFor(c => c).MustAsync(async (Command, collection)
                => !await _services.IsExsitValidation(Command.Name, Command.City,Command.Id))
                .WithMessage("Station Name and city Already Exsits ");

        }
    }
}
