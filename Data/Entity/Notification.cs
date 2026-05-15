using Data.Enums;
using Data.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public  class Notification
    {
        public int Id { get; set; }
        public string Message  { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
        public NotificationType Type  { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
