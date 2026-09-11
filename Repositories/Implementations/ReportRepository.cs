using Microsoft.EntityFrameworkCore;
using T2508M_SEM_Test.Data;
using T2508M_SEM_Test.DTOs.Reports;
using T2508M_SEM_Test.Repositories.Interfaces;

namespace T2508M_SEM_Test.Repositories.Implementations;

public class ReportRepository : IReportRepository
{
    private readonly ComicSystemContext _context;

    public ReportRepository(ComicSystemContext context)
    {
        _context = context;
    }

    public async Task<List<RentalReportDto>> GetRentalReportAsync(
        DateTime startDate,
        DateTime endDate)
    {
        var endDateExclusive = endDate.Date.AddDays(1);

        var report = await (
            from rental in _context.Rentals
            join customer in _context.Customers
                on rental.CustomerId equals customer.CustomerId

            join detail in _context.Rentaldetails
                on rental.RentalId equals detail.RentalId

            join comicBook in _context.Comicbooks
                on detail.ComicBookId equals comicBook.ComicBookId

            where rental.RentalDate >= startDate.Date
                  && rental.RentalDate < endDateExclusive

            orderby rental.RentalDate, comicBook.Title

            select new RentalReportDto
            {
                BookName = comicBook.Title,
                RentalDate = rental.RentalDate,
                ReturnDate = rental.ReturnDate,
                CustomerName = customer.FullName,
                Quantity = detail.Quantity
            }
        ).ToListAsync();

        for (int i = 0; i < report.Count; i++)
        {
            report[i].No = i + 1;
        }

        return report;
    }
}