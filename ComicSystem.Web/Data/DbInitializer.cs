using ComicSystem.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Web.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(ComicSystemDbContext context)
    {
        // Ensure database is created
        await context.Database.EnsureCreatedAsync();

        // Check if data already exists
        if (await context.ComicBooks.AnyAsync())
        {
            return; // DB has been seeded
        }

        // 1. Seed ComicBooks
        var comicBooks = new List<ComicBook>
        {
            new() { Title = "Conan", Author = "Gosho Aoyama", PricePerDay = 5000m },
            new() { Title = "Doraemon", Author = "Fujiko F. Fujio", PricePerDay = 4000m },
            new() { Title = "Dragon Ball", Author = "Akira Toriyama", PricePerDay = 6000m },
            new() { Title = "One Piece", Author = "Eiichiro Oda", PricePerDay = 7000m },
            new() { Title = "Naruto", Author = "Masashi Kishimoto", PricePerDay = 5500m },
            new() { Title = "Shin Cậu Bé Bút Chì", Author = "Yoshito Usui", PricePerDay = 4500m }
        };
        await context.ComicBooks.AddRangeAsync(comicBooks);
        await context.SaveChangesAsync();

        // 2. Seed Customers
        var customers = new List<Customer>
        {
            new()
            {
                FullName = "Nguyen Hung",
                PhoneNumber = "0901234567",
                RegistrationDate = new DateTime(2024, 9, 15, 9, 0, 0)
            },
            new()
            {
                FullName = "Tran Minh",
                PhoneNumber = "0987654321",
                RegistrationDate = new DateTime(2024, 9, 20, 14, 30, 0)
            },
            new()
            {
                FullName = "Le Thi Hoa",
                PhoneNumber = "0912345678",
                RegistrationDate = new DateTime(2024, 10, 1, 10, 0, 0)
            }
        };
        await context.Customers.AddRangeAsync(customers);
        await context.SaveChangesAsync();

        // 3. Seed Rentals & RentalDetails matching Exam Paper Example
        var conan = await context.ComicBooks.FirstAsync(b => b.Title == "Conan");
        var doraemon = await context.ComicBooks.FirstAsync(b => b.Title == "Doraemon");
        var customerHung = await context.Customers.FirstAsync(c => c.FullName == "Nguyen Hung");

        // Rental 1: Conan (01/10/2024 - 10/10/2024, Quantity: 1)
        var rental1 = new Rental
        {
            CustomerID = customerHung.CustomerID,
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

        // Rental 2: Doraemon (01/10/2024 - 20/10/2024, Quantity: 3)
        var rental2 = new Rental
        {
            CustomerID = customerHung.CustomerID,
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
