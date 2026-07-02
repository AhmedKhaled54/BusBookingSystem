using Core.Base;
using Data.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Trips.Command.Models
{
    public  class ChangeTripStatusCommand:IRequest<Response<string>>
    {
        public int TripId { get; set; }
        public TripeStatus Status { get; set; }
    }
}
