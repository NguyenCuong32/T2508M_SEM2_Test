using ACMF_Final.Application.Interfaces;
using ACMF_Final.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ACMF_Final.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // GET: Reports
        public IActionResult Index()
        {
            var viewModel = new ReportViewModel();
            return View(viewModel);
        }

        // POST: Reports
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ReportViewModel viewModel)
        {
            if (viewModel.EndDate < viewModel.StartDate)
            {
                ModelState.AddModelError("EndDate", "End date must be after start date.");
                return View(viewModel);
            }

            var results = await _reportService.GetRentalReportAsync(viewModel.StartDate, viewModel.EndDate);
            viewModel.Results = results.ToList();
            return View(viewModel);
        }
    }
}
