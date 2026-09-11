using Microsoft.AspNetCore.Mvc;
using T2508M_SEM_Test.Services.Interfaces;

namespace T2508M_SEM_Test.Controllers;

public class ReportsController : Controller
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    // GET: /Reports
    public IActionResult Index()
    {
        ViewBag.StartDate = DateTime.Today.AddDays(-30)
            .ToString("yyyy-MM-dd");

        ViewBag.EndDate = DateTime.Today
            .ToString("yyyy-MM-dd");

        ViewBag.HasSearched = false;

        return View();
    }

    // POST: /Reports
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(
        DateTime startDate,
        DateTime endDate)
    {
        if (endDate < startDate)
        {
            ModelState.AddModelError(
                string.Empty,
                "End date cannot be earlier than start date.");

            ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate.ToString("yyyy-MM-dd");
            ViewBag.HasSearched = true;

            return View(new List<T2508M_SEM_Test.DTOs.Reports.RentalReportDto>());
        }

        var report = await _reportService.GetRentalReportAsync(
            startDate,
            endDate);

        ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
        ViewBag.EndDate = endDate.ToString("yyyy-MM-dd");
        ViewBag.HasSearched = true;

        return View(report);
    }
}