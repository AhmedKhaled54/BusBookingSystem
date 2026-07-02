using Core.Feature.Route.Command.Models;
using FluentValidation;
using Services.Services.StationsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Route.Command.Validator
{
    public class CreateRouteCommandValidator : AbstractValidator<CreateRouteCommand>
    {
        private readonly IStaitonServices _services;
        public CreateRouteCommandValidator(IStaitonServices services)
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name Is Required !");
            RuleFor(c => c.Distance).GreaterThan(0).WithMessage("Distance Must Be greater than 0");
            RuleFor(c => c.StartStatoin).GreaterThan(0).WithMessage("StartStation Must Be greater than 0 ");
            RuleFor(c => c.EndStatoin).GreaterThan(0).WithMessage("EndStation Must Be greater than 0 ");

            RuleFor(c => c).Must(c => c.StartStatoin != c.EndStatoin)
                .WithMessage("StartStation and EndStation cannot be the same");
            Apply();
            _services = services;
        }

        private void Apply()
        {
            RuleFor(c => c.StartStatoin).MustAsync(StationExsit).WithMessage("StartStation does Not Exsit ");
            RuleFor(c => c.EndStatoin).MustAsync(StationExsit).WithMessage("EndStation does Not Exsit ");
        }

        private async Task<bool> StationExsit(int id, CancellationToken ct)
            => await _services.StationExsit(id);
    }
}
