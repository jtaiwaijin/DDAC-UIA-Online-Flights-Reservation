using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using uiaflights.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace uiaflights.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Flight>()
                .HasOne(f => f.Origin).WithMany(f => f.Departing).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Flight>()
                .HasOne(f => f.Dest).WithMany(f => f.Arriving).OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Airport> Airports { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
