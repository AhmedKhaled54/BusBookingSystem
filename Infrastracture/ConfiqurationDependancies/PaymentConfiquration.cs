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
    public class PaymentConfiquration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> b)
        {

            b.HasKey(c => c.Id);

            b.Property(c => c.Amount).HasPrecision(18, 2);
            b.Property(c=>c.Currency).HasMaxLength(3).IsRequired();
            b.Property(c=>c.TransactionId).HasMaxLength(120);

            
        }
    }
}
