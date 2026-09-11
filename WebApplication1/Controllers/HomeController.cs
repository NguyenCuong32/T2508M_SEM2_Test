using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IComicBookService _comicService;
        private readonly ICustomerService _customerService;
        private readonly IRentalService _rentalService;

        public HomeController(IComicBookService comicService, ICustomerService customerService, IRentalService rentalService)
        {
            _comicService = comicService;
            _customerService = customerService;
            _rentalService = rentalService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _comicService.GetAllComicBooksAsync();
            var customers = await _customerService.GetAllCustomersAsync();
            var rentals = await _rentalService.GetAllRentalsAsync();

            ViewBag.TotalBooks = books.Count();
            ViewBag.TotalCustomers = customers.Count();
            ViewBag.TotalRentals = rentals.Count();
            ViewBag.RecentRentals = rentals.Take(5).ToList();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
