using ACMF_PRACTICE_1.Data;
using ACMF_PRACTICE_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ACMF_PRACTICE_1.Controllers
{
    public class ReportController : Controller
    {
        private readonly AppDbContext _context;

        public ReportController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Report
        public IActionResult Index()
        {
            return View(new RentalReportViewModel());
        }

        // POST: Report
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RentalReportViewModel model)
        {
            if (model.StartDate.HasValue && model.EndDate.HasValue)
            {
                if (model.EndDate < model.StartDate)
                {
                    ModelState.AddModelError("EndDate", "End date must be on or after start date.");
                    return View(model);
                }

                var endDateInclusive = model.EndDate.Value.Date.AddDays(1);

                var rows = await _context.RentalDetails
                    .Include(rd => rd.ComicBook)
                    .Include(rd => rd.Rental)
                        .ThenInclude(r => r!.Customer)
                    .Where(rd =>
                        rd.Rental != null &&
                        rd.Rental.RentalDate >= model.StartDate.Value.Date &&
                        rd.Rental.RentalDate < endDateInclusive)
                    .OrderBy(rd => rd.Rental == null ? DateTime.MinValue : rd.Rental.RentalDate)
                    .Select(rd => new RentalReportRow
                    {
                        BookName     = rd.ComicBook == null ? "" : rd.ComicBook.Title,
                        RentalDate   = rd.Rental == null ? DateTime.MinValue : rd.Rental.RentalDate,
                        ReturnDate   = rd.Rental == null ? DateTime.MinValue : rd.Rental.ReturnDate,
                        CustomerName = rd.Rental == null || rd.Rental.Customer == null ? "" : rd.Rental.Customer.FullName,
                        Quantity     = rd.Quantity
                    })
                    .ToListAsync();

                // Assign sequential row numbers
                for (int i = 0; i < rows.Count; i++)
                    rows[i].No = i + 1;

                model.Rows = rows;
            }

            return View(model);
        }
    }
}
