using Data.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public abstract class BaseEntity
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
    }
}

