using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models.ViewModels;

namespace ComicSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ComicSystemDbContext _context;

        public ReportsController(ComicSystemDbContext context)
        {
            _context = context;
        }

        // GET: Reports (Question 4: Report all book rents between start date to end date)
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            // Default range if not specified: from beginning of 2024 to end of current year so sample data always displays
            var filterStart = startDate ?? new DateTime(2024, 10, 1);
            var filterEnd = endDate ?? new DateTime(DateTime.Now.Year + 1, 12, 31);

            // Query rental details between start date and end date
            var query = _context.RentalDetails
                .Include(rd => rd.Rental)
                    .ThenInclude(r => r!.Customer)
                .Include(rd => rd.ComicBook)
                .Where(rd => rd.Rental!.RentalDate >= filterStart 
                          && rd.Rental.RentalDate <= filterEnd.Date.AddDays(1).AddTicks(-1))
                .OrderBy(rd => rd.Rental!.RentalDate)
                .ThenBy(rd => rd.ComicBook!.Title);

            var list = await query.ToListAsync();

            int index = 1;
            var reportItems = list.Select(rd => new RentalReportItemViewModel
            {
                No = index++,
                BookName = rd.ComicBook?.Title ?? string.Empty,
                RentalDate = rd.Rental?.RentalDate ?? DateTime.MinValue,
                ReturnDate = rd.Rental?.ReturnDate ?? DateTime.MinValue,
                CustomerName = rd.Rental?.Customer?.FullName ?? string.Empty,
                Quantity = rd.Quantity,
                PricePerDay = rd.PricePerDay
            }).ToList();

            var model = new RentalReportViewModel
            {
                StartDate = startDate ?? filterStart,
                EndDate = endDate ?? DateTime.Today,
                ReportItems = reportItems
            };

            return View(model);
        }

        // GET: Reports/ExportCsv
        public async Task<IActionResult> ExportCsv(DateTime? startDate, DateTime? endDate)
        {
            var filterStart = startDate ?? new DateTime(2024, 10, 1);
            var filterEnd = endDate ?? new DateTime(DateTime.Now.Year + 1, 12, 31);

            var query = _context.RentalDetails
                .Include(rd => rd.Rental)
                    .ThenInclude(r => r!.Customer)
                .Include(rd => rd.ComicBook)
                .Where(rd => rd.Rental!.RentalDate >= filterStart 
                          && rd.Rental.RentalDate <= filterEnd.Date.AddDays(1).AddTicks(-1))
                .OrderBy(rd => rd.Rental!.RentalDate)
                .ThenBy(rd => rd.ComicBook!.Title);

            var list = await query.ToListAsync();

            var builder = new StringBuilder();
            // UTF-8 BOM for Excel support
            builder.AppendLine("No,Book name,Rental date,Return date,Customer name,Quantity");

            int index = 1;
            foreach (var item in list)
            {
                builder.AppendLine($"{index++},\"{item.ComicBook?.Title}\",{item.Rental?.RentalDate:dd/MM/yyyy},{item.Rental?.ReturnDate:dd/MM/yyyy},\"{item.Rental?.Customer?.FullName}\",{item.Quantity}");
            }

            var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(builder.ToString())).ToArray();
            return File(bytes, "text/csv", $"ComicRents_Report_{DateTime.Now:yyyyMMdd}.csv");
        }
    }
}
