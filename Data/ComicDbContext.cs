using Microsoft.EntityFrameworkCore;
using ComicSystem.Models;

namespace ComicSystem.Data
{
    public class ComicDbContext : DbContext
    {
        public ComicDbContext(DbContextOptions<ComicDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<ComicBook> ComicBooks { get; set; } = null!;
        public DbSet<Rental> Rentals { get; set; } = null!;
        public DbSet<RentalDetail> RentalDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customer configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");
                entity.HasKey(e => e.CustomerID);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(15);
            });

            // ComicBook configuration
            modelBuilder.Entity<ComicBook>(entity =>
            {
                entity.ToTable("ComicBooks");
                entity.HasKey(e => e.ComicBookID);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Author).IsRequired().HasMaxLength(255);
                entity.Property(e => e.PricePerDay).HasColumnType("decimal(10, 2)");
            });

            // Rental configuration
            modelBuilder.Entity<Rental>(entity =>
            {
                entity.ToTable("Rentals");
                entity.HasKey(e => e.RentalID);
                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.CustomerID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // RentalDetail configuration
            modelBuilder.Entity<RentalDetail>(entity =>
            {
                entity.ToTable("RentalDetails");
                entity.HasKey(e => e.RentalDetailID);
                entity.Property(e => e.PricePerDay).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Rental)
                    .WithMany(p => p.RentalDetails)
                    .HasForeignKey(d => d.RentalID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.ComicBook)
                    .WithMany(p => p.RentalDetails)
                    .HasForeignKey(d => d.ComicBookID)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
