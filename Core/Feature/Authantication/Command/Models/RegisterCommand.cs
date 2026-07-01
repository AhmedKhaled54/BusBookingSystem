using Core.Base;
using Data.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.Command.Models
{
    public  class RegisterCommand:IRequest<Response<string>>
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password),ErrorMessage ="Password Not Matching !")]
        public string ConfirmPassword  { get; set; }
        public string NationalId { get; set; }
        public DateOnly BirthDate { get; set; }
        public GenderType Gender { get; set; }
    }
}
