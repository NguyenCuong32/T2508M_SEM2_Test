using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;
using MyMvcApp.ViewModels;

namespace MyMvcApp.Repositories;

public class ComicRepository(AppDbContext db)
{
    public Task<List<ComicBook>> GetBooksAsync() =>
        db.ComicBooks.AsNoTracking().OrderBy(b => b.Title).ToListAsync();

    public Task<ComicBook?> GetBookAsync(int id) =>
        db.ComicBooks.FirstOrDefaultAsync(b => b.ComicBookId == id);

    public Task<bool> BookHasRentalsAsync(int id) =>
        db.RentalDetails.AnyAsync(d => d.ComicBookId == id);

    public async Task AddBookAsync(ComicBook book)
    {
        db.ComicBooks.Add(book);
        await db.SaveChangesAsync();
    }

    public Task SaveChangesAsync() => db.SaveChangesAsync();

    public async Task DeleteBookAsync(ComicBook book)
    {
        db.ComicBooks.Remove(book);
        await db.SaveChangesAsync();
    }

    public Task<List<Customer>> GetCustomersAsync() =>
        db.Customers.AsNoTracking().OrderBy(c => c.FullName).ToListAsync();

    public Task<bool> CustomerExistsAsync(int id) =>
        db.Customers.AnyAsync(c => c.CustomerId == id);

    public async Task AddCustomerAsync(Customer customer)
    {
        db.Customers.Add(customer);
        await db.SaveChangesAsync();
    }

    public Task<List<Rental>> GetRentalsAsync() =>
        db.Rentals.AsNoTracking()
            .Include(r => r.Customer)
            .Include(r => r.RentalDetails).ThenInclude(d => d.ComicBook)
            .OrderByDescending(r => r.RentalId).ToListAsync();

    public async Task AddRentalAsync(Rental rental)
    {
        db.Rentals.Add(rental);
        await db.SaveChangesAsync();
    }

    public Task<List<RentalReportRow>> GetReportAsync(DateTime startDate, DateTime endDate) =>
        db.RentalDetails.AsNoTracking()
            .Where(d => d.Rental.RentalDate.Date >= startDate.Date
                && d.Rental.RentalDate.Date <= endDate.Date)
            .OrderBy(d => d.Rental.RentalDate).ThenBy(d => d.RentalDetailId)
            .Select(d => new RentalReportRow
            {
                BookName = d.ComicBook.Title,
                RentalDate = d.Rental.RentalDate,
                ReturnDate = d.Rental.ReturnDate,
                CustomerName = d.Rental.Customer.FullName,
                Quantity = d.Quantity
            }).ToListAsync();
}
