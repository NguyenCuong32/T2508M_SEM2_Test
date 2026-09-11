using Microsoft.EntityFrameworkCore;

namespace EXAM.Models
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

            // 1. Bảng Customers
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");
                entity.HasKey(e => e.CustomerID);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
                entity.Property(e => e.RegisterDate).HasColumnType("datetime");
            });

            // 2. Bảng ComicBooks
            modelBuilder.Entity<ComicBook>(entity =>
            {
                entity.ToTable("ComicBooks");
                entity.HasKey(e => e.ComicBookID);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Author).HasMaxLength(255);
                entity.Property(e => e.PricePerDay).HasColumnType("decimal(10,2)").IsRequired();
            });

            // 3. Bảng Rentals
            modelBuilder.Entity<Rental>(entity =>
            {
                entity.ToTable("Rentals");
                entity.HasKey(e => e.RentalID);
                entity.Property(e => e.RentalDate).HasColumnType("datetime").IsRequired();
                entity.Property(e => e.ReturnDate).HasColumnType("datetime");
                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(e => e.Customer)
                    .WithMany(c => c.Rentals)
                    .HasForeignKey(e => e.CustomerID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // 4. Bảng RentalDetails
            modelBuilder.Entity<RentalDetail>(entity =>
            {
                entity.ToTable("RentalDetails");
                entity.HasKey(e => e.RentalDetailID);
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.PricePerDay).HasColumnType("decimal(10,2)").IsRequired();

                entity.HasOne(e => e.Rental)
                    .WithMany(r => r.RentalDetails)
                    .HasForeignKey(e => e.RentalID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ComicBook)
                    .WithMany(c => c.RentalDetails)
                    .HasForeignKey(e => e.ComicBookID)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

