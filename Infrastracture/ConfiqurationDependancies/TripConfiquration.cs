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
    public class TripConfiquration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> b)
        {
            b.HasKey(c => c.Id);

            b.HasOne(c => c.Bus)
                .WithMany(c=>c.Trips)
                .HasForeignKey(c => c.BusId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(c => c.Routes)
               .WithMany(c=>c.Trips)
               .HasForeignKey(c => c.RouteId)
               .OnDelete(DeleteBehavior.Restrict);


            b.HasMany(c => c.Reviews)
                .WithOne(c => c.Trip)
                .HasForeignKey(c => c.TripId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(c=>c.Driver)
                .WithMany()
                .HasForeignKey(c=>c.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(c => c.DepartualTime);
        }
    }
}
