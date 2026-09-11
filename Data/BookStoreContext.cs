using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {
        }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<ComicBooks> ComicBooks { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ComicBooks>()
                .Property(x => x.PricePerDay)
                .HasPrecision(10, 2);

            modelBuilder.Entity<RentalDetail>()
                .HasOne(rd => rd.ComicBook)
                .WithMany(cb => cb.RentalDetails)
                .HasForeignKey(rd => rd.ComicBookId);

            modelBuilder.Entity<Rental>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.Rentals)
                .HasForeignKey(x => x.CustomerId);

            modelBuilder.Entity<RentalDetail>()
                .Property(x => x.PricePerDay)
                .HasPrecision(10, 2);
        }
    }
}