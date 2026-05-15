using Data.Entity;
using Data.Entity.Drivers;
using Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, int,
        IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {

        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Seats> Seats { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingSeats> BookingSeats { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Stations> Stations { get; set; }
        public DbSet<Routes> Routes { get; set; }
        public DbSet<RouteStations> RouteStations { get; set; }
        public DbSet<DriverApplication> DriverApplications { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Notification> Notifications { get; set; } 

}
}
