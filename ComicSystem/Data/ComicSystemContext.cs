using Microsoft.EntityFrameworkCore;
using ComicSystem.Models;

namespace ComicSystem.Data
{
    public class ComicSystemContext : DbContext
    {
        public ComicSystemContext(DbContextOptions<ComicSystemContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ComicBook> ComicBooks { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RentalDetail>()
                .HasOne(rd => rd.Rental)
                .WithMany(r => r.RentalDetails)
                .HasForeignKey(rd => rd.RentalID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RentalDetail>()
                .HasOne(rd => rd.ComicBook)
                .WithMany(cb => cb.RentalDetails)
                .HasForeignKey(rd => rd.ComicBookID)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data
            modelBuilder.Entity<ComicBook>().HasData(
                new ComicBook { ComicBookID = 1, Title = "Conan", Author = "Gosho Aoyama", PricePerDay = 2.50m },
                new ComicBook { ComicBookID = 2, Title = "Doraemon", Author = "Fujiko F. Fujio", PricePerDay = 2.00m },
                new ComicBook { ComicBookID = 3, Title = "Dragon Ball", Author = "Akira Toriyama", PricePerDay = 3.00m },
                new ComicBook { ComicBookID = 4, Title = "Naruto", Author = "Masashi Kishimoto", PricePerDay = 2.50m },
                new ComicBook { ComicBookID = 5, Title = "One Piece", Author = "Eiichiro Oda", PricePerDay = 3.50m }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerID = 1, FullName = "Nguyen Hung", PhoneNumber = "0901234567", Registration = new DateTime(2024, 1, 1) },
                new Customer { CustomerID = 2, FullName = "Tran Van A", PhoneNumber = "0912345678", Registration = new DateTime(2024, 2, 15) }
            );
        }
    }
}
