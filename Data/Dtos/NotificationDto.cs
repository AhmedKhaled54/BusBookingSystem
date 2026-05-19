using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public record NotificationDto(
       Guid Guid,
       string Message,
       string Type,
       bool IsRead,
       DateTime CreatedAtUtc,
       int UserId
   );
}
