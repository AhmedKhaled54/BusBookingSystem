
using Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastracture.RealTime.Interfaces
{
    public  interface INotificationClient
    {
        Task NotificationReceived(NotificationDto notification);

    }
}
