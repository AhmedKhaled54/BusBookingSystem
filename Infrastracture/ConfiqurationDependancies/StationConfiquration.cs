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
    public class StationConfiquration : IEntityTypeConfiguration<Stations>
    {
        public void Configure(EntityTypeBuilder<Stations> b)
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.Name).IsRequired();
            b.Property(c => c.City).IsRequired();
            b.Property(c => c.City).IsRequired();
            b.Property(c => c.Latitude).HasPrecision(10, 7).IsRequired();
            b.Property(c => c.Longitude).HasPrecision(10, 7).IsRequired();

            b.HasIndex(c => new { c.Name, c.City }).IsUnique();

        }
    }
}
