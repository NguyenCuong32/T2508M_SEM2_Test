using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models;

namespace MyMvcApp.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ComicBook> ComicBooks => Set<ComicBook>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Rental> Rentals => Set<Rental>();
    public DbSet<RentalDetail> RentalDetails => Set<RentalDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasCharSet("utf8mb4").UseCollation("utf8mb4_unicode_ci");
        modelBuilder.Entity<ComicBook>().Property(b => b.PricePerDay).HasPrecision(10, 2);
        modelBuilder.Entity<RentalDetail>().Property(d => d.PricePerDay).HasPrecision(10, 2);
        modelBuilder.Entity<Rental>().Property(r => r.Status).HasMaxLength(50);
        modelBuilder.Entity<Rental>().HasOne(r => r.Customer).WithMany()
            .HasForeignKey(r => r.CustomerId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<RentalDetail>().HasOne(d => d.ComicBook).WithMany()
            .HasForeignKey(d => d.ComicBookId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<RentalDetail>().HasOne(d => d.Rental).WithMany(r => r.RentalDetails)
            .HasForeignKey(d => d.RentalId).OnDelete(DeleteBehavior.Cascade);
    }
}
