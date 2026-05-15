using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.ConfiqurationDependancies
{
    public class SeatsConfiquration : IEntityTypeConfiguration<Seats>
    {
        public void Configure(EntityTypeBuilder<Seats> b)
        {
            b.HasKey(x => x.Id);
            b.Property(c=>c.Number).IsRequired();
            b.HasIndex(c=>new {c.Number,c.BusId});

            
        }
    }
}
