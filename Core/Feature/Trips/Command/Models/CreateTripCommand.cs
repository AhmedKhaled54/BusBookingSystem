using Core.Base;
using Data.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Trips.Command.Models
{
    public class CreateTripCommand : IRequest<Response<string>>
    {
        public DateTime DepartualTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public TripeStatus Status { get; set; }
        public int DriverId { get; set; }
        public int BusId { get; set; }
        public int RouteId { get; set; }


    }
}
