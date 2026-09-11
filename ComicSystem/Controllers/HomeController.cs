using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComicSystem.Models;
using ComicSystem.Services.Interfaces;

namespace ComicSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IComicBookService _comicBookService;
        private readonly ICustomerService _customerService;
        private readonly IRentalService _rentalService;

        public HomeController(
            IComicBookService comicBookService,
            ICustomerService customerService,
            IRentalService rentalService)
        {
            _comicBookService = comicBookService;
            _customerService = customerService;
            _rentalService = rentalService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _comicBookService.GetAllBooksAsync();
            var customers = await _customerService.GetAllCustomersAsync();
            var rentals = await _rentalService.GetAllRentalsAsync();

            ViewBag.TotalBooks = books.Count();
            ViewBag.TotalCustomers = customers.Count();
            ViewBag.ActiveRentals = rentals.Count(r => r.Status == "Đang thuê");
            ViewBag.TotalRentals = rentals.Count();

            return View();
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
