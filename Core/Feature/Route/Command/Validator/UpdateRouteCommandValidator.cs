using Core.Feature.Route.Command.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Route.Command.Validator
{
    public  class UpdateRouteCommandValidator:AbstractValidator<UpdateRouteCommand>
    {
        public UpdateRouteCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name Is Required !");
            RuleFor(c => c.Distance).GreaterThan(0).WithMessage("Distance Must Be greater than 0");
            RuleFor(c => c.StartStatoin).GreaterThan(0).WithMessage("StartStation Must Be greater than 0 ");
            RuleFor(c => c.EndStatoin).GreaterThan(0).WithMessage("EndStation Must Be greater than 0 ");

            RuleFor(c => c).Must(c => c.StartStatoin != c.EndStatoin)
                .WithMessage("StartStation and EndStation cannot be the same");

        }
    }
}
