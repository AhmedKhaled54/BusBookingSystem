using Infrastracture.RealTime.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.RealTime.Hubs
{
    public  class NotificationHub:Hub<INotificationClient>
    {
        public override async Task OnConnectedAsync()
        {
            var userid = Context.UserIdentifier;
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user-{userid}");
        }

    }
}
