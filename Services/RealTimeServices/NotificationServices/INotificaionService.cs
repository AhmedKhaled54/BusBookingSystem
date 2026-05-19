using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RealTimeServices.NotificationServices
{
    public  interface INotificaionService
    {
        Task SendNotification(int userid, string message, NotificationType type);
    }
}
