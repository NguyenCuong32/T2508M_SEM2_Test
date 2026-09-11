using Microsoft.EntityFrameworkCore;

namespace finaltest.Models
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

            // Table mappings
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<ComicBook>().ToTable("ComicBooks");
            modelBuilder.Entity<Rental>().ToTable("Rentals");
            modelBuilder.Entity<RentalDetail>().ToTable("RentalDetails");

            // Relationships
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
                .WithMany(b => b.RentalDetails)
                .HasForeignKey(rd => rd.ComicBookID)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public void SeedInitialData()
        {
            if (!ComicBooks.Any())
            {
                ComicBooks.AddRange(
                    new ComicBook { Title = "Conan", Author = "Gosho Aoyama", PricePerDay = 5000 },
                    new ComicBook { Title = "Doraemon", Author = "Fujiko F. Fujio", PricePerDay = 4000 },
                    new ComicBook { Title = "Dragon Ball", Author = "Akira Toriyama", PricePerDay = 6000 },
                    new ComicBook { Title = "One Piece", Author = "Eiichiro Oda", PricePerDay = 7000 },
                    new ComicBook { Title = "Naruto", Author = "Masashi Kishimoto", PricePerDay = 5500 }
                );
                SaveChanges();
            }

            if (!Customers.Any())
            {
                Customers.AddRange(
                    new Customer { FullName = "Nguyen Hung", PhoneNumber = "0912345678", RegistrationDate = new DateTime(2024, 9, 1) },
                    new Customer { FullName = "Tran Van B", PhoneNumber = "0987654321", RegistrationDate = new DateTime(2024, 9, 15) },
                    new Customer { FullName = "Le Thi C", PhoneNumber = "0901122334", RegistrationDate = new DateTime(2024, 10, 1) }
                );
                SaveChanges();
            }

            if (!Rentals.Any())
            {
                var customer = Customers.FirstOrDefault(c => c.FullName == "Nguyen Hung");
                var conan = ComicBooks.FirstOrDefault(b => b.Title == "Conan");
                var doraemon = ComicBooks.FirstOrDefault(b => b.Title == "Doraemon");

                if (customer != null && conan != null && doraemon != null)
                {
                    var rental1 = new Rental
                    {
                        CustomerID = customer.CustomerID,
                        RentalDate = new DateTime(2024, 10, 1),
                        ReturnDate = new DateTime(2024, 10, 10),
                        Status = "Đang thuê"
                    };
                    Rentals.Add(rental1);
                    SaveChanges();

                    RentalDetails.Add(new RentalDetail
                    {
                        RentalID = rental1.RentalID,
                        ComicBookID = conan.ComicBookID,
                        Quantity = 1,
                        PricePerDay = conan.PricePerDay
                    });

                    var rental2 = new Rental
                    {
                        CustomerID = customer.CustomerID,
                        RentalDate = new DateTime(2024, 10, 1),
                        ReturnDate = new DateTime(2024, 10, 20),
                        Status = "Đang thuê"
                    };
                    Rentals.Add(rental2);
                    SaveChanges();

                    RentalDetails.Add(new RentalDetail
                    {
                        RentalID = rental2.RentalID,
                        ComicBookID = doraemon.ComicBookID,
                        Quantity = 3,
                        PricePerDay = doraemon.PricePerDay
                    });

                    SaveChanges();
                }
            }
        }
    }
}
