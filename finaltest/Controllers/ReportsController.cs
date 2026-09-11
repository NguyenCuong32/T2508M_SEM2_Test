using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using finaltest.Models;

namespace finaltest.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ComicDbContext _context;

        public ReportsController(ComicDbContext context)
        {
            _context = context;
        }

        // GET: Reports or Reports/Index?startDate=2024-10-01&endDate=2024-10-31
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            var query = _context.RentalDetails
                .Include(rd => rd.Rental)
                    .ThenInclude(r => r!.Customer)
                .Include(rd => rd.ComicBook)
                .AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(rd => rd.Rental!.RentalDate >= startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                query = query.Where(rd => rd.Rental!.RentalDate <= endDate.Value.Date);
            }

            var rentalDetails = await query
                .OrderBy(rd => rd.Rental!.RentalDate)
                .ThenBy(rd => rd.RentalDetailID)
                .ToListAsync();

            int count = 1;
            var reportItems = rentalDetails.Select(rd => new RentalReportItemViewModel
            {
                No = count++,
                BookName = rd.ComicBook?.Title ?? "N/A",
                RentalDate = rd.Rental?.RentalDate ?? DateTime.MinValue,
                ReturnDate = rd.Rental?.ReturnDate ?? DateTime.MinValue,
                CustomerName = rd.Rental?.Customer?.FullName ?? "N/A",
                Quantity = rd.Quantity,
                PricePerDay = rd.PricePerDay
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
}
