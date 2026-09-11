using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using finaltest_ACMF.Models;

namespace finaltest_ACMF.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ComicDbContext _context;

        public ReportsController(ComicDbContext context)
        {
            _context = context;
        }

        // GET: Reports (Question 4)
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            var model = new RentalReportViewModel
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var query = _context.RentalDetails
                .Include(rd => rd.Rental)
                    .ThenInclude(r => r!.Customer)
                .Include(rd => rd.ComicBook)
                .AsQueryable();

            if (startDate.HasValue)
            {
                var start = startDate.Value.Date;
                query = query.Where(rd => rd.Rental != null && rd.Rental.RentalDate >= start);
            }

            if (endDate.HasValue)
            {
                // Include entire end date up to end of day
                var end = endDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(rd => rd.Rental != null && rd.Rental.RentalDate <= end);
            }

            var results = await query
                .OrderBy(rd => rd.Rental!.RentalDate)
                .ThenBy(rd => rd.RentalDetailID)
                .Select(rd => new RentalReportItem
                {
                    BookName = rd.ComicBook != null ? rd.ComicBook.Title : string.Empty,
                    RentalDate = rd.Rental != null ? rd.Rental.RentalDate : DateTime.MinValue,
                    ReturnDate = rd.Rental != null ? rd.Rental.ReturnDate : DateTime.MinValue,
                    CustomerName = rd.Rental != null && rd.Rental.Customer != null ? rd.Rental.Customer.FullName : string.Empty,
                    Quantity = rd.Quantity,
                    PricePerDay = rd.PricePerDay,
                    Status = rd.Rental != null ? rd.Rental.Status : string.Empty
                })
                .ToListAsync();

            // Set 1-based sequential number (No)
            for (int i = 0; i < results.Count; i++)
            {
                results[i].No = i + 1;
            }

            model.ReportItems = results;
            return View(model);
        }
    }
}
