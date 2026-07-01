using Core.Feature.Authantication.Command.Models;
using FluentValidation;
using Pipelines.Sockets.Unofficial;
using Services.Services.Authantication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.Command.Validator
{
    public class AddLoginValidator : AbstractValidator<LoginCommand>
    {
        private readonly IAuthanticationServices _services;
        public AddLoginValidator(IAuthanticationServices services)
        {

            RuleFor(c => c.Email).NotEmpty().WithMessage("Please Enter Your Email");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Please Enter Your Password ");
            CustomApply();
            _services = services;
        }


        public void CustomApply()
        {
            RuleFor(c => c.Email).MustAsync(async (email, comcelection) => await _services.IsExsit(email))
                .WithMessage("Email Not Exsit Please Try Again !");

        }

    }
}
