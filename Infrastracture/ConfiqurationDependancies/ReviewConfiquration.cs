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
    public class ReviewConfiquration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> b)
        {

            b.HasKey(c => c.Id);
            b.Property(c =>c .UserId).IsRequired();
            b.Property(c =>c .Rate).IsRequired();
            b.Property(c =>c .Comment).HasMaxLength(1000).IsRequired();
            
            b.HasIndex(c => new {c .UserId,c .TripId });
           
            b.HasIndex(c => new {c .UserId,c .DriverId });

            b.ToTable( c => 
            c.HasCheckConstraint("CK_Reviews_Rating", "[Rate] BETWEEN 1 AND 5"));
        }
    }
}
