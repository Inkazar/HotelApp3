using HotelApp3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=DannesHotelAppDb;Trusted_Connection=True;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer relationships
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Bookings)
                .WithOne(b => b.Customer)
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Room relationships
            modelBuilder.Entity<Room>()
                .HasMany(r => r.Bookings)
                .WithOne(b => b.Room)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Booking relationships
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>().HasData(
                new Room { RoomId = 1, Type = "Single", MaxCapacity = 1, PricePerNight = 500 },
                new Room { RoomId = 2, Type = "Double", MaxCapacity = 2, PricePerNight = 800 },
                new Room { RoomId = 3, Type = "Double", MaxCapacity = 2, PricePerNight = 900 },
                new Room { RoomId = 4, Type = "Double", MaxCapacity = 4, PricePerNight = 1200 }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, Name = "Alice Smith", Email = "alice@example.com", Phone = "123456789" },
                new Customer { CustomerId = 2, Name = "Bob Johnson", Email = "bob@example.com", Phone = "987654321" },
                new Customer { CustomerId = 3, Name = "Charlie Brown", Email = "charlie@example.com", Phone = "456789123" },
                new Customer { CustomerId = 4, Name = "Diana Ross", Email = "diana@example.com", Phone = "789123456" }
            );
        }
    }
}
