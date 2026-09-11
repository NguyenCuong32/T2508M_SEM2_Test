using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using finaltest_ACMF.Models;

namespace finaltest_ACMF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ComicDbContext _context;

        public HomeController(ComicDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalBooks = await _context.ComicBooks.CountAsync();
            ViewBag.TotalCustomers = await _context.Customers.CountAsync();
            ViewBag.ActiveRentals = await _context.Rentals.CountAsync(r => r.Status == "Đang thuê");
            ViewBag.TotalRentals = await _context.Rentals.CountAsync();

            var recentRentals = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .OrderByDescending(r => r.RentalDate)
                .Take(5)
                .ToListAsync();

            return View(recentRentals);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
