using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using T2508M_SEM_Test.Models;

namespace T2508M_SEM_Test.Data;

public partial class ComicSystemContext : DbContext
{
    public ComicSystemContext(DbContextOptions<ComicSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comicbook> Comicbooks { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Rental> Rentals { get; set; }

    public virtual DbSet<Rentaldetail> Rentaldetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comicbook>(entity =>
        {
            entity.HasKey(e => e.ComicBookId).HasName("PRIMARY");

            entity.ToTable("comicbooks");

            entity.Property(e => e.ComicBookId)
                .HasColumnType("int(11)")
                .HasColumnName("ComicBookID");
            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.PricePerDay).HasPrecision(10);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customers");

            entity.Property(e => e.CustomerId)
                .HasColumnType("int(11)")
                .HasColumnName("CustomerID");
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.RentalId).HasName("PRIMARY");

            entity.ToTable("rentals");

            entity.HasIndex(e => e.CustomerId, "FK_Rentals_Customers");

            entity.Property(e => e.RentalId)
                .HasColumnType("int(11)")
                .HasColumnName("RentalID");
            entity.Property(e => e.CustomerId)
                .HasColumnType("int(11)")
                .HasColumnName("CustomerID");
            entity.Property(e => e.RentalDate).HasColumnType("datetime");
            entity.Property(e => e.ReturnDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Rentals_Customers");
        });

        modelBuilder.Entity<Rentaldetail>(entity =>
        {
            entity.HasKey(e => e.RentalDetailId).HasName("PRIMARY");

            entity.ToTable("rentaldetails");

            entity.HasIndex(e => e.ComicBookId, "FK_RentalDetails_ComicBooks");

            entity.HasIndex(e => e.RentalId, "FK_RentalDetails_Rentals");

            entity.Property(e => e.RentalDetailId)
                .HasColumnType("int(11)")
                .HasColumnName("RentalDetailID");
            entity.Property(e => e.ComicBookId)
                .HasColumnType("int(11)")
                .HasColumnName("ComicBookID");
            entity.Property(e => e.PricePerDay).HasPrecision(10);
            entity.Property(e => e.Quantity).HasColumnType("int(11)");
            entity.Property(e => e.RentalId)
                .HasColumnType("int(11)")
                .HasColumnName("RentalID");

            entity.HasOne(d => d.ComicBook).WithMany(p => p.Rentaldetails)
                .HasForeignKey(d => d.ComicBookId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_RentalDetails_ComicBooks");

            entity.HasOne(d => d.Rental).WithMany(p => p.Rentaldetails)
                .HasForeignKey(d => d.RentalId)
                .HasConstraintName("FK_RentalDetails_Rentals");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
