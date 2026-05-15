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
    public class BookingConfiquration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> b)
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.BookingNumber).IsRequired();
            b.Property(c => c.TotalPrice).HasPrecision(18, 2);

            b.HasIndex(c => c.BookingNumber).IsUnique();

            
            
            b.HasOne(c=>c.Trip)
                .WithMany()
                .HasForeignKey(c=>c.TripId) 
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.Payment).WithOne(x => x.Booking)
                .HasForeignKey<Payment>(x => x.BookingId)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.Ticket)
                .WithOne(x => x.Booking)
                .HasForeignKey<Ticket>(x => x.BookingId)
                .OnDelete(DeleteBehavior.NoAction);




        }
    }
}
