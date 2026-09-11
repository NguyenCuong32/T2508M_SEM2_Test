using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ComicDbContext : DbContext
    {
        public ComicDbContext(DbContextOptions<ComicDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ComicBook> ComicBooks { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table mapping
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<ComicBook>().ToTable("ComicBooks");
            modelBuilder.Entity<Rental>().ToTable("Rentals");
            modelBuilder.Entity<RentalDetail>().ToTable("RentalDetails");

            // Decimal precision matching diagram: decimal(10, 2)
            modelBuilder.Entity<ComicBook>()
                .Property(c => c.PricePerDay)
                .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<RentalDetail>()
                .Property(rd => rd.PricePerDay)
                .HasColumnType("decimal(10, 2)");

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
                .WithMany(cb => cb.RentalDetails)
                .HasForeignKey(rd => rd.ComicBookID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
