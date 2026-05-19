using Core.Feature.Authantication.Command.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.Command.Validator
{
    public class AddChangePasswordValidation : AbstractValidator<ChangePasswordCommand>
    {
        public AddChangePasswordValidation()
        {
            RuleFor(c => c.CurrentPassword).NotEmpty().WithMessage("Please Insert Your Password!");
            RuleFor(c => c.NewPassword).NotEmpty();
            RuleFor(c => c.Confirm_NewPassword) .NotEmpty()
                .Equal(c => c.NewPassword)
                .WithMessage("Password confirmation does not match.");
        }
    }
}
