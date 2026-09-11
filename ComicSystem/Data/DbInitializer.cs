using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Models;

namespace ComicSystem.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(ComicSystemDbContext context)
        {
            // Ensure database is created
            await context.Database.MigrateAsync();

            if (await context.ComicBooks.AnyAsync())
            {
                return; // DB already seeded
            }

            // 1. Seed ComicBooks
            var conan = new ComicBook { Title = "Conan", Author = "Gosho Aoyama", PricePerDay = 5000m };
            var doraemon = new ComicBook { Title = "Doraemon", Author = "Fujiko F. Fujio", PricePerDay = 4000m };
            var dragonBall = new ComicBook { Title = "Dragon Ball", Author = "Akira Toriyama", PricePerDay = 6000m };
            var onePiece = new ComicBook { Title = "One Piece", Author = "Eiichiro Oda", PricePerDay = 7000m };
            var naruto = new ComicBook { Title = "Naruto", Author = "Masashi Kishimoto", PricePerDay = 5000m };

            await context.ComicBooks.AddRangeAsync(conan, doraemon, dragonBall, onePiece, naruto);
            await context.SaveChangesAsync();

            // 2. Seed Customers
            var nguyenHung = new Customer
            {
                FullName = "Nguyen Hung",
                PhoneNumber = "0912345678",
                RegistrationDate = new DateTime(2024, 10, 1, 8, 30, 0)
            };

            var tranAn = new Customer
            {
                FullName = "Tran Van An",
                PhoneNumber = "0987654321",
                RegistrationDate = new DateTime(2024, 10, 5, 9, 15, 0)
            };

            var leMai = new Customer
            {
                FullName = "Le Thi Mai",
                PhoneNumber = "0905123456",
                RegistrationDate = new DateTime(2024, 10, 15, 14, 0, 0)
            };

            await context.Customers.AddRangeAsync(nguyenHung, tranAn, leMai);
            await context.SaveChangesAsync();

            // 3. Seed Sample Rentals as specified in the Exam Paper
            // Rental 1: Conan, 1 book, 01/10/2024 to 10/10/2024, Nguyen Hung
            var rental1 = new Rental
            {
                CustomerID = nguyenHung.CustomerID,
                RentalDate = new DateTime(2024, 10, 1),
                ReturnDate = new DateTime(2024, 10, 10),
                Status = "Đang thuê"
            };
            await context.Rentals.AddAsync(rental1);
            await context.SaveChangesAsync();

            var detail1 = new RentalDetail
            {
                RentalID = rental1.RentalID,
                ComicBookID = conan.ComicBookID,
                Quantity = 1,
                PricePerDay = conan.PricePerDay
            };
            await context.RentalDetails.AddAsync(detail1);

            // Rental 2: Doraemon, 3 books, 01/10/2024 to 20/10/2024, Nguyen Hung
            var rental2 = new Rental
            {
                CustomerID = nguyenHung.CustomerID,
                RentalDate = new DateTime(2024, 10, 1),
                ReturnDate = new DateTime(2024, 10, 20),
                Status = "Đang thuê"
            };
            await context.Rentals.AddAsync(rental2);
            await context.SaveChangesAsync();

            var detail2 = new RentalDetail
            {
                RentalID = rental2.RentalID,
                ComicBookID = doraemon.ComicBookID,
                Quantity = 3,
                PricePerDay = doraemon.PricePerDay
            };
            await context.RentalDetails.AddAsync(detail2);

            await context.SaveChangesAsync();
        }
    }
}
