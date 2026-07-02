using Core.Base;
using MediatR;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Stations.Command.Models
{
    public  class DeletedSoftStationCommand:IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int UserId   { get; set; }
    }
}
