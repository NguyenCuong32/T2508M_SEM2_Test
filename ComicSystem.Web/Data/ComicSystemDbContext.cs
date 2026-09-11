using Microsoft.EntityFrameworkCore;
using ComicSystem.Web.Models;

namespace ComicSystem.Web.Data;

public class ComicSystemDbContext : DbContext
{
    public ComicSystemDbContext(DbContextOptions<ComicSystemDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<ComicBook> ComicBooks => Set<ComicBook>();
    public DbSet<Rental> Rentals => Set<Rental>();
    public DbSet<RentalDetail> RentalDetails => Set<RentalDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Customers table
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customers");
            entity.HasKey(e => e.CustomerID);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(15);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
        });

        // Configure ComicBooks table
        modelBuilder.Entity<ComicBook>(entity =>
        {
            entity.ToTable("ComicBooks");
            entity.HasKey(e => e.ComicBookID);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Author).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PricePerDay).HasColumnType("decimal(10, 2)");
        });

        // Configure Rentals table
        modelBuilder.Entity<Rental>(entity =>
        {
            entity.ToTable("Rentals");
            entity.HasKey(e => e.RentalID);
            entity.Property(e => e.RentalDate).HasColumnType("datetime");
            entity.Property(e => e.ReturnDate).HasColumnType("datetime");
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);

            entity.HasOne(e => e.Customer)
                  .WithMany(c => c.Rentals)
                  .HasForeignKey(e => e.CustomerID)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure RentalDetails table
        modelBuilder.Entity<RentalDetail>(entity =>
        {
            entity.ToTable("RentalDetails");
            entity.HasKey(e => e.RentalDetailID);
            entity.Property(e => e.PricePerDay).HasColumnType("decimal(10, 2)");

            entity.HasOne(e => e.Rental)
                  .WithMany(r => r.RentalDetails)
                  .HasForeignKey(e => e.RentalID)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.ComicBook)
                  .WithMany(b => b.RentalDetails)
                  .HasForeignKey(e => e.ComicBookID)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
