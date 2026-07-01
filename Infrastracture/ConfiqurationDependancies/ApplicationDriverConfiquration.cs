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
    public class ApplicationDriverConfiquration : IEntityTypeConfiguration<DriverApplication>
    {
        public void Configure(EntityTypeBuilder<DriverApplication> b)
        {
            
            b.Property(c => c.Email).IsRequired();
            b.Property(c => c.FullName).IsRequired();
            b.Property(c => c.UserId).IsRequired();
            b.Property(c => c.LicenceNumber).IsRequired().HasMaxLength(10);
            b.Property(c => c.LicenceImageUrl).IsRequired();
            b.Property(c => c.LicenceExprireYear).IsRequired();

            b.HasIndex(c => new { c.UserId, c.Status });
            
            b.HasIndex(c => c.LicenceNumber).IsUnique();
           
            
            
            
            b.ToTable(c =>
            {
                c.HasCheckConstraint("CK_Driver_LicenceNumber", "LicenceNumber Like 'DR-[0-9][0-9][0-9][0-9]'");
            });


        }
    }
}
