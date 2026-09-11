using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComicSystem.Services.Interfaces;

namespace ComicSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // GET: Reports/Index
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            // Default dates: 01/10/2024 to current date (covers exam sample data and any newly added records)
            var start = startDate ?? new DateTime(2024, 10, 1);
            var end = endDate ?? DateTime.Today;

            if (end < start)
            {
                ModelState.AddModelError(string.Empty, "Ngày kết thúc không được nhỏ hơn ngày bắt đầu.");
                end = start;
            }

            var report = await _reportService.GetRentalReportAsync(start, end);
            return View(report);
        }
    }
}
