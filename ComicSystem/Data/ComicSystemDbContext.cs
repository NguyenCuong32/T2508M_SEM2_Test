using Microsoft.EntityFrameworkCore;
using ComicSystem.Models;

namespace ComicSystem.Data
{
    public class ComicSystemDbContext : DbContext
    {
        public ComicSystemDbContext(DbContextOptions<ComicSystemDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<ComicBook> ComicBooks { get; set; } = null!;
        public DbSet<Rental> Rentals { get; set; } = null!;
        public DbSet<RentalDetail> RentalDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Precision configurations for decimal fields
            modelBuilder.Entity<ComicBook>()
                .Property(c => c.PricePerDay)
                .HasPrecision(10, 2);

            modelBuilder.Entity<RentalDetail>()
                .Property(rd => rd.PricePerDay)
                .HasPrecision(10, 2);

            // Relationships
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RentalDetail>()
                .HasOne(rd => rd.Rental)
                .WithMany(r => r.RentalDetails)
                .HasForeignKey(rd => rd.RentalID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RentalDetail>()
                .HasOne(rd => rd.ComicBook)
                .WithMany(b => b.RentalDetails)
                .HasForeignKey(rd => rd.ComicBookID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
