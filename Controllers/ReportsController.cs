using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.ViewModels;

namespace ComicSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ComicDbContext _context;

        public ReportsController(ComicDbContext context)
        {
            _context = context;
        }

        // GET: Reports
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
                // Include the full day of endDate
                var endDay = endDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(rd => rd.Rental!.RentalDate <= endDay);
            }

            var rentalDetailsList = await query
                .OrderBy(rd => rd.Rental!.RentalDate)
                .ThenBy(rd => rd.RentalID)
                .ToListAsync();

            var reportItems = new List<ReportItemViewModel>();
            int stt = 1;

            foreach (var detail in rentalDetailsList)
            {
                reportItems.Add(new ReportItemViewModel
                {
                    No = stt++,
                    BookName = detail.ComicBook?.Title ?? "N/A",
                    RentalDate = detail.Rental?.RentalDate ?? DateTime.MinValue,
                    ReturnDate = detail.Rental?.ReturnDate ?? DateTime.MinValue,
                    CustomerName = detail.Rental?.Customer?.FullName ?? "N/A",
                    Quantity = detail.Quantity
                });
            }

            var viewModel = new ReportFilterViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                Reports = reportItems
            };

            return View(viewModel);
        }
    }
}
