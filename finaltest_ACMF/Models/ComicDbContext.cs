using System;
using Microsoft.EntityFrameworkCore;

namespace finaltest_ACMF.Models
{
    public class ComicDbContext : DbContext
    {
        public ComicDbContext(DbContextOptions<ComicDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<ComicBook> ComicBooks { get; set; } = null!;
        public virtual DbSet<Rental> Rentals { get; set; } = null!;
        public virtual DbSet<RentalDetail> RentalDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Customer
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerID);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(15);
                entity.Property(e => e.RegistrationDate).HasDefaultValueSql("GETDATE()");
            });

            // Configure ComicBook
            modelBuilder.Entity<ComicBook>(entity =>
            {
                entity.HasKey(e => e.ComicBookID);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Author).IsRequired().HasMaxLength(255);
                entity.Property(e => e.PricePerDay).HasColumnType("decimal(10, 2)").IsRequired();
            });

            // Configure Rental
            modelBuilder.Entity<Rental>(entity =>
            {
                entity.HasKey(e => e.RentalID);
                entity.Property(e => e.RentalDate).IsRequired();
                entity.Property(e => e.ReturnDate).IsRequired();
                entity.Property(e => e.Status).HasMaxLength(50).HasDefaultValue("Đang thuê");

                entity.HasOne(d => d.Customer)
                      .WithMany(p => p.Rentals)
                      .HasForeignKey(d => d.CustomerID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure RentalDetail
            modelBuilder.Entity<RentalDetail>(entity =>
            {
                entity.HasKey(e => e.RentalDetailID);
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.PricePerDay).HasColumnType("decimal(10, 2)").IsRequired();

                entity.HasOne(d => d.Rental)
                      .WithMany(p => p.RentalDetails)
                      .HasForeignKey(d => d.RentalID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.ComicBook)
                      .WithMany(p => p.RentalDetails)
                      .HasForeignKey(d => d.ComicBookID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Seed Initial Data
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerID = 1, FullName = "Nguyen Hung", PhoneNumber = "0912345678", RegistrationDate = new DateTime(2024, 9, 1, 8, 30, 0) },
                new Customer { CustomerID = 2, FullName = "Tran Van An", PhoneNumber = "0987654321", RegistrationDate = new DateTime(2024, 9, 5, 9, 15, 0) },
                new Customer { CustomerID = 3, FullName = "Le Thi Mai", PhoneNumber = "0905123456", RegistrationDate = new DateTime(2024, 9, 10, 14, 0, 0) }
            );

            modelBuilder.Entity<ComicBook>().HasData(
                new ComicBook { ComicBookID = 1, Title = "Conan", Author = "Gosho Aoyama", PricePerDay = 5000m },
                new ComicBook { ComicBookID = 2, Title = "Doraemon", Author = "Fujiko F. Fujio", PricePerDay = 4000m },
                new ComicBook { ComicBookID = 3, Title = "Dragon Ball", Author = "Akira Toriyama", PricePerDay = 6000m },
                new ComicBook { ComicBookID = 4, Title = "One Piece", Author = "Eiichiro Oda", PricePerDay = 5500m },
                new ComicBook { ComicBookID = 5, Title = "Naruto", Author = "Masashi Kishimoto", PricePerDay = 5000m }
            );

            modelBuilder.Entity<Rental>().HasData(
                new Rental { RentalID = 1, CustomerID = 1, RentalDate = new DateTime(2024, 10, 1, 8, 0, 0), ReturnDate = new DateTime(2024, 10, 10, 17, 0, 0), Status = "Đã trả" },
                new Rental { RentalID = 2, CustomerID = 1, RentalDate = new DateTime(2024, 10, 1, 9, 30, 0), ReturnDate = new DateTime(2024, 10, 20, 17, 0, 0), Status = "Đã trả" },
                new Rental { RentalID = 3, CustomerID = 2, RentalDate = new DateTime(2024, 10, 15, 10, 0, 0), ReturnDate = new DateTime(2024, 10, 25, 18, 0, 0), Status = "Đang thuê" }
            );

            modelBuilder.Entity<RentalDetail>().HasData(
                new RentalDetail { RentalDetailID = 1, RentalID = 1, ComicBookID = 1, Quantity = 1, PricePerDay = 5000m },
                new RentalDetail { RentalDetailID = 2, RentalID = 2, ComicBookID = 2, Quantity = 3, PricePerDay = 4000m },
                new RentalDetail { RentalDetailID = 3, RentalID = 3, ComicBookID = 3, Quantity = 2, PricePerDay = 6000m }
            );
        }
    }
}
