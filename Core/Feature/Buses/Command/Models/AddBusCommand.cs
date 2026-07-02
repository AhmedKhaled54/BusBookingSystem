using Core.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Buses.Command.Models
{
    public  class AddBusCommand:IRequest<Response<string>>
    {
        
        public string Model { get; set; }
        public string PlateNumber { get; set; }
        public IFormFile Image { get; set; }
        public int Capacity { get; set; }
        public int DriverId { get; set; }
        public int OwnerId { get; set; }


    }
}
