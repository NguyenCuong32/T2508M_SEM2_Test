using ACMF_Final.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ACMF_Final.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ComicBook> ComicBooks { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customer configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.CustomerID);
                entity.Property(c => c.FullName).IsRequired().HasMaxLength(100);
                entity.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(15);
            });

            // ComicBook configuration
            modelBuilder.Entity<ComicBook>(entity =>
            {
                entity.HasKey(c => c.ComicBookID);
                entity.Property(c => c.Title).IsRequired().HasMaxLength(200);
                entity.Property(c => c.Author).IsRequired().HasMaxLength(100);
                entity.Property(c => c.PricePerDay).HasColumnType("decimal(18,2)");
            });

            // Rental configuration
            modelBuilder.Entity<Rental>(entity =>
            {
                entity.HasKey(r => r.RentalID);
                entity.Property(r => r.Status).IsRequired().HasMaxLength(20);

                entity.HasOne(r => r.Customer)
                      .WithMany(c => c.Rentals)
                      .HasForeignKey(r => r.CustomerID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // RentalDetail configuration
            modelBuilder.Entity<RentalDetail>(entity =>
            {
                entity.HasKey(rd => rd.RentalDetailID);
                entity.Property(rd => rd.PricePerDay).HasColumnType("decimal(18,2)");

                entity.HasOne(rd => rd.Rental)
                      .WithMany(r => r.RentalDetails)
                      .HasForeignKey(rd => rd.RentalID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(rd => rd.ComicBook)
                      .WithMany()
                      .HasForeignKey(rd => rd.ComicBookID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Seed data
            modelBuilder.Entity<ComicBook>().HasData(
                new ComicBook { ComicBookID = 1, Title = "Dragon Ball", Author = "Akira Toriyama", PricePerDay = 5000 },
                new ComicBook { ComicBookID = 2, Title = "One Piece", Author = "Eiichiro Oda", PricePerDay = 6000 },
                new ComicBook { ComicBookID = 3, Title = "Naruto", Author = "Masashi Kishimoto", PricePerDay = 5500 },
                new ComicBook { ComicBookID = 4, Title = "Doraemon", Author = "Fujiko F. Fujio", PricePerDay = 4000 },
                new ComicBook { ComicBookID = 5, Title = "Conan", Author = "Gosho Aoyama", PricePerDay = 5000 }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerID = 1, FullName = "Nguyen Van A", PhoneNumber = "0901234567", RegistrationDate = new DateTime(2026, 1, 15) },
                new Customer { CustomerID = 2, FullName = "Tran Thi B", PhoneNumber = "0912345678", RegistrationDate = new DateTime(2026, 2, 20) },
                new Customer { CustomerID = 3, FullName = "Le Van C", PhoneNumber = "0923456789", RegistrationDate = new DateTime(2026, 3, 10) }
            );
        }
    }
}
