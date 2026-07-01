using Core.Base;
using MediatR;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Buses.Command.Models
{
    public  class RestoreBusCommand:IRequest<Response<string>>
    {
        public int Id { get; set; }

        public RestoreBusCommand(int id)
        {
            Id = id;
        }
    }
}
