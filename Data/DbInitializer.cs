using System;
using System.Linq;
using ComicSystem.Models;

namespace ComicSystem.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ComicDbContext context)
        {
            // Ensure database schema is created
            context.Database.EnsureCreated();

            // Seed Customers if empty
            if (!context.Customers.Any())
            {
                var customers = new Customer[]
                {
                    new Customer { FullName = "Nguyen Hung", PhoneNumber = "0912345678", RegisterDate = new DateTime(2024, 1, 10) },
                    new Customer { FullName = "Tran Van A", PhoneNumber = "0987654321", RegisterDate = new DateTime(2024, 2, 15) }
                };

                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            // Seed ComicBooks if empty
            if (!context.ComicBooks.Any())
            {
                var comicBooks = new ComicBook[]
                {
                    new ComicBook { Title = "Conan", Author = "Gosho Aoyama", PricePerDay = 5000 },
                    new ComicBook { Title = "Doraemon", Author = "Fujiko F. Fujio", PricePerDay = 4000 },
                    new ComicBook { Title = "Dragon Ball", Author = "Akira Toriyama", PricePerDay = 6000 },
                    new ComicBook { Title = "One Piece", Author = "Eiichiro Oda", PricePerDay = 7000 }
                };

                context.ComicBooks.AddRange(comicBooks);
                context.SaveChanges();
            }

            // Seed Rentals & RentalDetails if empty
            if (!context.Rentals.Any())
            {
                var customer = context.Customers.FirstOrDefault(c => c.FullName == "Nguyen Hung") ?? context.Customers.First();
                var conan = context.ComicBooks.FirstOrDefault(b => b.Title == "Conan") ?? context.ComicBooks.First();
                var doraemon = context.ComicBooks.FirstOrDefault(b => b.Title == "Doraemon") ?? context.ComicBooks.First();

                var rental1 = new Rental
                {
                    CustomerID = customer.CustomerID,
                    RentalDate = new DateTime(2024, 10, 1),
                    ReturnDate = new DateTime(2024, 10, 10),
                    Status = "Đang thuê"
                };

                var rental2 = new Rental
                {
                    CustomerID = customer.CustomerID,
                    RentalDate = new DateTime(2024, 10, 1),
                    ReturnDate = new DateTime(2024, 10, 20),
                    Status = "Đang thuê"
                };

                context.Rentals.AddRange(rental1, rental2);
                context.SaveChanges();

                var detail1 = new RentalDetail
                {
                    RentalID = rental1.RentalID,
                    ComicBookID = conan.ComicBookID,
                    Quantity = 1,
                    PricePerDay = conan.PricePerDay
                };

                var detail2 = new RentalDetail
                {
                    RentalID = rental2.RentalID,
                    ComicBookID = doraemon.ComicBookID,
                    Quantity = 3,
                    PricePerDay = doraemon.PricePerDay
                };

                context.RentalDetails.AddRange(detail1, detail2);
                context.SaveChanges();
            }
        }
    }
}
