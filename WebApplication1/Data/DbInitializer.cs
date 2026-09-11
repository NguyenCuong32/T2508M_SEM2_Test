using System;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ComicDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any comic books.
            if (context.ComicBooks.Any())
            {
                return;   // DB has been seeded
            }

            var conan = new ComicBook
            {
                Title = "Conan",
                Author = "Gosho Aoyama",
                PricePerDay = 5000m
            };

            var doraemon = new ComicBook
            {
                Title = "Doraemon",
                Author = "Fujiko F. Fujio",
                PricePerDay = 4000m
            };

            var dragonBall = new ComicBook
            {
                Title = "Dragon Ball",
                Author = "Akira Toriyama",
                PricePerDay = 6000m
            };

            var onePiece = new ComicBook
            {
                Title = "One Piece",
                Author = "Eiichiro Oda",
                PricePerDay = 7000m
            };

            context.ComicBooks.AddRange(conan, doraemon, dragonBall, onePiece);
            context.SaveChanges();

            // Seed Customers
            var customer1 = new Customer
            {
                FullName = "NguyenHung",
                PhoneNumber = "0987654321",
                RegistrationDate = new DateTime(2024, 1, 10, 8, 30, 0)
            };

            var customer2 = new Customer
            {
                FullName = "Tran Minh Anh",
                PhoneNumber = "0912345678",
                RegistrationDate = new DateTime(2024, 5, 15, 9, 0, 0)
            };

            context.Customers.AddRange(customer1, customer2);
            context.SaveChanges();

            // Seed Rentals and RentalDetails matching the exam prompt table
            var rental1 = new Rental
            {
                CustomerID = customer1.CustomerID,
                RentalDate = new DateTime(2024, 10, 1),
                ReturnDate = new DateTime(2024, 10, 10),
                Status = "Đang thuê"
            };

            var rental2 = new Rental
            {
                CustomerID = customer1.CustomerID,
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
