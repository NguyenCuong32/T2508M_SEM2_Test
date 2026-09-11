using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;

namespace ComicSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ComicSystemContext _context;

        public ReportsController(ComicSystemContext context)
        {
            _context = context;
        }

        // GET: Reports
        public IActionResult Index()
        {
            var model = new ReportViewModel
            {
                StartDate = DateTime.Today.AddMonths(-1),
                EndDate = DateTime.Today
            };
            return View(model);
        }

        // POST: Reports
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                var endDateInclusive = model.EndDate.Date.AddDays(1);

                var results = await _context.RentalDetails
                    .Include(rd => rd.Rental)
                        .ThenInclude(r => r!.Customer)
                    .Include(rd => rd.ComicBook)
                    .Where(rd => rd.Rental!.RentalDate >= model.StartDate.Date
                              && rd.Rental.RentalDate < endDateInclusive)
                    .OrderBy(rd => rd.Rental!.RentalDate)
                    .Select(rd => new ReportItem
                    {
                        BookName = rd.ComicBook!.Title,
                        RentalDate = rd.Rental!.RentalDate,
                        ReturnDate = rd.Rental.ReturnDate,
                        CustomerName = rd.Rental.Customer!.FullName,
                        Quantity = rd.Quantity
                    })
                    .ToListAsync();

                // Add row numbers
                int no = 1;
                foreach (var item in results)
                {
                    item.No = no++;
                }

                model.Results = results;
            }

            return View(model);
        }
    }
}
