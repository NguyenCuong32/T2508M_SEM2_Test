using System;
using System.Linq;
using ComicSystem.Models;

namespace ComicSystem.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ComicSystemDbContext context)
        {
            // Ensure database is created (if not already existing)
            try
            {
                context.Database.EnsureCreated();
            }
            catch
            {
                // Tables might already exist from SQL script
            }

            // Check if data already exists
            if (context.Customers.Any() && context.ComicBooks.Any())
            {
                return; // DB has already been seeded
            }

            // Seed Customers
            if (!context.Customers.Any())
            {
                var customers = new Customer[]
                {
                    new Customer
                    {
                        FullName = "Nguyen Hung",
                        PhoneNumber = "0987654321",
                        RegistrationDate = new DateTime(2024, 10, 1, 8, 0, 0)
                    },
                    new Customer
                    {
                        FullName = "Tran Van An",
                        PhoneNumber = "0912345678",
                        RegistrationDate = new DateTime(2024, 10, 5, 9, 30, 0)
                    },
                    new Customer
                    {
                        FullName = "Le Thi Mai",
                        PhoneNumber = "0933888999",
                        RegistrationDate = new DateTime(2024, 10, 10, 14, 15, 0)
                    }
                };
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            // Seed ComicBooks
            if (!context.ComicBooks.Any())
            {
                var comicBooks = new ComicBook[]
                {
                    new ComicBook { Title = "Conan", Author = "Gosho Aoyama", PricePerDay = 5000m },
                    new ComicBook { Title = "Doraemon", Author = "Fujiko F. Fujio", PricePerDay = 4000m },
                    new ComicBook { Title = "Dragon Ball", Author = "Akira Toriyama", PricePerDay = 6000m },
                    new ComicBook { Title = "One Piece", Author = "Eiichiro Oda", PricePerDay = 5500m },
                    new ComicBook { Title = "Naruto", Author = "Masashi Kishimoto", PricePerDay = 5000m }
                };
                context.ComicBooks.AddRange(comicBooks);
                context.SaveChanges();
            }

            // Seed Rentals & RentalDetails (matching Question 4 exam paper)
            if (!context.Rentals.Any())
            {
                var hung = context.Customers.FirstOrDefault(c => c.FullName == "Nguyen Hung");
                var conan = context.ComicBooks.FirstOrDefault(b => b.Title == "Conan");
                var doraemon = context.ComicBooks.FirstOrDefault(b => b.Title == "Doraemon");

                if (hung != null && conan != null && doraemon != null)
                {
                    // Rental 1: Nguyen Hung rents Conan (Qty: 1)
                    var rental1 = new Rental
                    {
                        CustomerID = hung.CustomerID,
                        RentalDate = new DateTime(2024, 10, 1, 8, 0, 0),
                        ReturnDate = new DateTime(2024, 10, 10, 17, 0, 0),
                        Status = "Đang thuê"
                    };
                    context.Rentals.Add(rental1);
                    context.SaveChanges();

                    var detail1 = new RentalDetail
                    {
                        RentalID = rental1.RentalID,
                        ComicBookID = conan.ComicBookID,
                        Quantity = 1,
                        PricePerDay = conan.PricePerDay
                    };
                    context.RentalDetails.Add(detail1);

                    // Rental 2: Nguyen Hung rents Doraemon (Qty: 3)
                    var rental2 = new Rental
                    {
                        CustomerID = hung.CustomerID,
                        RentalDate = new DateTime(2024, 10, 1, 9, 0, 0),
                        ReturnDate = new DateTime(2024, 10, 20, 17, 0, 0),
                        Status = "Đang thuê"
                    };
                    context.Rentals.Add(rental2);
                    context.SaveChanges();

                    var detail2 = new RentalDetail
                    {
                        RentalID = rental2.RentalID,
                        ComicBookID = doraemon.ComicBookID,
                        Quantity = 3,
                        PricePerDay = doraemon.PricePerDay
                    };
                    context.RentalDetails.Add(detail2);

                    context.SaveChanges();
                }
            }
        }
    }
}
