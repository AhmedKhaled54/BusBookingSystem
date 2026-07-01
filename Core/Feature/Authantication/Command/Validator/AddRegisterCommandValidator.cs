using Core.Feature.Authantication.Command.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.Command.Validator
{
    public  class AddRegisterCommandValidator:AbstractValidator<RegisterCommand>
    {
        public AddRegisterCommandValidator()
        {
            RuleFor(c => c.First_Name).NotEmpty().WithMessage("Please Enter FirstName ");
            RuleFor(c => c.Last_Name).NotEmpty().WithMessage("Please Enter LastName ");
            RuleFor(c => c.Email).EmailAddress().WithMessage("Please enter a valid email address (e.g., user@example.com)")
                .NotEmpty().WithMessage("Email Is Required! ");
            RuleFor(c => c.Gender).IsInEnum().WithMessage("Invalid Gender Value !")
                .NotEmpty().WithMessage("Gander Is Required !");
            RuleFor(c => c.BirthDate).NotEmpty().WithMessage("BirthDate Is Required ")
                .Must(c => DateTime.Now.Year - c.Year >= 12).WithMessage("You Must be  at least 18 Years Old! ");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Password Is Required!");
            RuleFor(c => c.ConfirmPassword).NotEmpty().WithMessage("ConfirmPassword Is Required!");

        }
    }
}
