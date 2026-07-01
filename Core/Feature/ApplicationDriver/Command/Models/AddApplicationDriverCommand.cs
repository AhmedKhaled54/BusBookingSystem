using Core.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.ApplicationDriver.Command.Models
{
    public  class AddApplicationDriverCommand:IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string LicenceNumber { get; set; }
        public DateOnly LicenceExprireYear { get; set; }
        public IFormFile LicenceImageUrl { get; set; }
        public int NationalId { get; set; }
        public int ExperienceYears { get; set; }
    }
}
