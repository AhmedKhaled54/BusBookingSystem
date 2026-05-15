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
    public class TicketConfiquration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> b)
        {
            b.HasKey(t => t.Id);

            b.Property(c=>c.Number).IsRequired();
            b.HasIndex(c=>c.Number).IsUnique();
            b.Property(c=>c.QRCode).HasMaxLength(300);

        }
    }
}
