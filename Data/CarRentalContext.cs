using Microsoft.EntityFrameworkCore;
using CarRentalAPI.Models;

namespace CarRentalAPI.Data
{
    public class CarRentalContext : DbContext
    {
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Auto> Autos { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }

        public CarRentalContext(DbContextOptions<CarRentalContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Auto)
                .WithMany() // Relación con Auto
                .HasForeignKey(r => r.AutoId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Client)
                .WithMany() // Relación con Client
                .HasForeignKey(r => r.ClientId);
        }
    }

}

