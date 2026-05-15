using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Identity
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? RevokedOn { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => RevokedOn == null && !IsExpired;
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
