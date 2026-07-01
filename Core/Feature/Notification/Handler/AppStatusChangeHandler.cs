using Core.Feature.Notification.Events.EventDriverStatus;
using Data.Dtos;
using Infrastracture.RealTime.Hubs;
using Infrastracture.RealTime.Interfaces;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Notification.Handler
{
    public class AppStatusChangeHandler : INotificationHandler<AppStatusChangeEvents>
    {
        private readonly IHubContext<NotificationHub, INotificationClient> _hub;

        public AppStatusChangeHandler(IHubContext<NotificationHub, INotificationClient> hub)
        {
            _hub = hub;
        }

        public async Task Handle(AppStatusChangeEvents notification, CancellationToken cancellationToken)
        {

            await _hub.Clients.User(notification.UserId.ToString()).NotificationReceived(
                new NotificationDto(Guid.NewGuid(), notification.message, notification.Status, false, DateTime.Now, notification.UserId));


        }
    }
}
