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
    public class RoutesConfiquration : IEntityTypeConfiguration<Routes>
    {
        public void Configure(EntityTypeBuilder<Routes> b)
        {
            b.HasKey(c => c.Id);

            b.Property(c => c.Name).IsRequired();

            b.HasOne(c => c.StartStation)
                .WithMany()
                .HasForeignKey(c => c.StartStationId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(b => b.EndStation)
                .WithMany()
                .HasForeignKey(c => c.EndStationId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasMany(c => c.RouteStations)
                .WithOne()
                .HasForeignKey(c => c.RouteId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
