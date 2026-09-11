using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Web.Data;
using ComicSystem.Web.Models.ViewModels;

namespace ComicSystem.Web.Controllers;

public class ReportsController : Controller
{
    private readonly ComicSystemDbContext _context;

    public ReportsController(ComicSystemDbContext context)
    {
        _context = context;
    }

    // GET: Reports (Question 4 - Report all book rents between start date to end date)
    public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
    {
        // Default to October 2024 if not specified, matching the exam paper demo period
        if (!startDate.HasValue && !endDate.HasValue)
        {
            startDate = new DateTime(2024, 10, 1);
            endDate = new DateTime(2024, 10, 31);
        }

        var query = _context.RentalDetails
            .Include(rd => rd.Rental)
                .ThenInclude(r => r!.Customer)
            .Include(rd => rd.ComicBook)
            .AsNoTracking();

        if (startDate.HasValue)
        {
            query = query.Where(rd => rd.Rental != null && rd.Rental.RentalDate >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            // Include until the end of the specified day
            var endOfDay = endDate.Value.Date.AddDays(1).AddTicks(-1);
            query = query.Where(rd => rd.Rental != null && rd.Rental.RentalDate <= endOfDay);
        }

        var details = await query
            .OrderBy(rd => rd.Rental!.RentalDate)
            .ThenBy(rd => rd.ComicBook!.Title)
            .ToListAsync();

        var reportItems = details.Select((rd, index) => new RentalReportItemViewModel
        {
            No = index + 1,
            BookName = rd.ComicBook?.Title ?? "N/A",
            RentalDate = rd.Rental?.RentalDate ?? DateTime.MinValue,
            ReturnDate = rd.Rental?.ReturnDate ?? DateTime.MinValue,
            CustomerName = rd.Rental?.Customer?.FullName ?? "N/A",
            Quantity = rd.Quantity
        }).ToList();

        var viewModel = new RentalReportViewModel
        {
            StartDate = startDate,
            EndDate = endDate,
            Items = reportItems
        };

        return View(viewModel);
    }
}
