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
    public class BusConfiquration : IEntityTypeConfiguration<Bus>
    {
        public void Configure(EntityTypeBuilder<Bus> b)
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.PlateNumber).IsRequired().HasMaxLength(10);
            b.HasIndex(c => c.PlateNumber).IsUnique();

            b.Property(c=>c.Model).IsRequired().HasMaxLength(50);
            
            
            b.HasOne(c=>c.Driver)
                .WithMany(c=>c.Buses)
                .HasForeignKey(c=>c.DriverId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasMany(c => c.Seats)
                .WithOne()
                .HasForeignKey(c => c.BusId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
