using Core.Feature.Stations.Command.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Stations.Command.Validatot
{
    public  class AddStationCommandValidatot:AbstractValidator<CreateStationCommand>
    {
        public AddStationCommandValidatot()
        {
            RuleFor(c => c.Latitude).NotEmpty().InclusiveBetween(-90, 90)
                .WithMessage("Latitude must be between -90 and 90"); ;
            RuleFor(c => c.Longitude).NotEmpty().InclusiveBetween(-180,180)
                .WithMessage("Longitude must be between -180 and 180"); ;

        }
    }
}
