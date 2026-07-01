using Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Trips.Command.Models
{
    public  class RestoreTripCommand:IRequest<Response<string>>
    {
        public int Id { get; set; }
        public RestoreTripCommand(int id)
        {
            Id = id;
        }
    
    }
}
