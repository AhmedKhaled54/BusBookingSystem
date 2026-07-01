using Data.Entity;
using Data.Enums;
using Infrastracture.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RealTimeServices.NotificationServices
{
    public class NotificationService : INotificaionService
    {
        private readonly IUnitOfWork _UOW;
        public NotificationService(IUnitOfWork uOW)
        {
            _UOW = uOW;
        }
        public async Task SendNotification(int userid, string message, NotificationType type)
        {

            var Not = new Notification
            {
                UserId = userid,
                Message = message,
                Type = type,
                IsRead = false
            };
            await _UOW.Repository<Notification>().AddAsync(Not);
        }   
    }
}
