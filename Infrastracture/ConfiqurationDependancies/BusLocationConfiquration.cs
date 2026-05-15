using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.ConfiqurationDependancies
{
    public class BusLocationConfiquration : IEntityTypeConfiguration<BusLocation>
    {
        public void Configure(EntityTypeBuilder<BusLocation> b)
        {

            b.HasKey(c => c.Id);

            b.Property(c => c.Latitude).HasPrecision(10, 7);
            b.Property(c=> c.Longitude).HasPrecision(10, 7);
            b.Property(c => c.SpeedKph).HasPrecision(8, 2);
            b.HasOne(c => c.Trip)
                .WithMany(c=>c.BusLocations)
                .HasForeignKey(x => x.TripId)
                .OnDelete(DeleteBehavior.Cascade);
            
            b.HasIndex(x => new { x.TripId, x.RecordedAt });
        


    }

    }
}
