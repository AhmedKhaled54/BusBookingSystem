using Data.Entity.Drivers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.ConfiqurationDependancies
{
    public class DriverConfiquration : IEntityTypeConfiguration<Driver>
    {
       

        public void Configure(EntityTypeBuilder<Driver> b)
        {

            b.Property(c => c.Email).IsRequired();
            b.Property(c => c.FullName).IsRequired();
            b.Property(c => c.UserId).IsRequired();
            b.Property(c => c.LicenceNumber).IsRequired().HasMaxLength(10);
            b.Property(c => c.LicenceImageUrl).IsRequired();
            b.Property(c => c.LicenceExprireYear).IsRequired();
            b.HasIndex(c => c.LicenceNumber).IsUnique();
            
            b.HasMany(c=>c.Reviews)
                .WithOne(c=>c.Driver)
                .HasForeignKey(c=>c.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            //b.HasOne(c => c.User)
            //    .WithOne(c => c.Driver)
            //    .HasForeignKey(c => c.)  to do in user 


            b.ToTable(c =>
            {
                c.HasCheckConstraint("CK_Driver_LicenceNumber", "LicenceNumber Like 'DR-[0-9][0-9][0-9][0-9]'");
            });
                
        }
    }
}
