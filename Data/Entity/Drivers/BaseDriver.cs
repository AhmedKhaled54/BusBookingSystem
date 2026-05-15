using Data.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity.Drivers
{
    public abstract class BaseDriver
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string  LicenceNumber { get; set; }
        public DateOnly LicenceExprireYear { get; set; }
        public string LicenceImageUrl { get; set; }
        public int NationalId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
