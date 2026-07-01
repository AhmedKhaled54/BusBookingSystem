using Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Buses.Command.Models
{
    public  class DeleteSoftBusCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public DeleteSoftBusCommand(int id)
        {
            Id = id;
        }
    }
}
