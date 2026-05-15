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
    public class RouteStationConfiquration : IEntityTypeConfiguration<RouteStations>
    {
        public void Configure(EntityTypeBuilder<RouteStations> b)
        {
            b.HasKey(c => c.Id);

            b.HasOne(c => c.Route)
                .WithMany()
                .HasForeignKey(c => c.RouteId);

           

        }
    }
}
