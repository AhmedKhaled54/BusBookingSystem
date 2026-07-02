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
    public class BookingSeatsConfiquration : IEntityTypeConfiguration<BookingSeats>
    {
        public void Configure(EntityTypeBuilder<BookingSeats> b)
        {
            b.HasKey(c => c.Id);

            b.Property(c => c.Price).HasPrecision(18, 2).IsRequired();

            b.HasOne(c => c.Seats)
                .WithMany(c=>c.BookingSeats)
                .HasForeignKey(c => c.SeatsId)
                .OnDelete(DeleteBehavior.NoAction);
            
            b.HasOne(c => c.Booking)
                .WithMany(c=>c.BookingSeats)
                .HasForeignKey(c => c.BookingId)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasIndex(c => new { c.BookingId, c.SeatsId });

        }
    }
}
