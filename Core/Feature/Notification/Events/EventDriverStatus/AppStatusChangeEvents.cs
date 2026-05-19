using MediatR;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Notification.Events.EventDriverStatus
{
    public  record AppStatusChangeEvents(int UserId ,string Status ,string message ):INotification;
}
