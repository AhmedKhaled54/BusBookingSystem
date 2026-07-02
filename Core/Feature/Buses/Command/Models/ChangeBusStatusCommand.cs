using Core.Base;
using Data.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Buses.Command.Models
{
    public  class ChangeBusStatusCommand:IRequest<Response<string>>
    {
        public int Id { get; set; }
        public BusStatus status { get; set; }
    }
}
