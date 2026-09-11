using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models.ViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class RentalsController : Controller
    {
        private readonly IRentalService _rentalService;
        private readonly ICustomerService _customerService;
        private readonly IComicBookService _comicBookService;

        public RentalsController(
            IRentalService rentalService,
            ICustomerService customerService,
            IComicBookService comicBookService)
        {
            _rentalService = rentalService;
            _customerService = customerService;
            _comicBookService = comicBookService;
        }

        // GET: Rentals (List of all rentals)
        public async Task<IActionResult> Index()
        {
            var rentals = await _rentalService.GetAllRentalsAsync();
            return View(rentals);
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var rental = await _rentalService.GetRentalByIdAsync(id.Value);
            if (rental == null) return NotFound();

            return View(rental);
        }

        // GET: Rentals/Create (Question 3)
        public async Task<IActionResult> Create()
        {
            var model = new RentalCreateViewModel
            {
                RentalDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddDays(7),
                Quantity = 1,
                Status = "Đang thuê"
            };

            await PopulateDropDownsAsync(model);
            return View(model);
        }

        // POST: Rentals/Create (Question 3)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentalCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var rental = await _rentalService.RentBookAsync(model);
                    TempData["SuccessMessage"] = $"Tạo phiếu thuê truyện (Mã: #{rental.RentalID}) thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Đã có lỗi xảy ra khi thuê truyện: " + ex.Message);
                }
            }

            await PopulateDropDownsAsync(model);
            return View(model);
        }

        // GET: Rentals/Report (Question 4)
        public async Task<IActionResult> Report(DateTime? startDate, DateTime? endDate)
        {
            // If both are null, we can default to whole month or display all
            var report = await _rentalService.GetReportAsync(startDate, endDate);
            return View(report);
        }

        // GET: Rentals/GetBookPrice/5 (Helper for AJAX dynamic price auto-fill)
        [HttpGet]
        public async Task<IActionResult> GetBookPrice(int id)
        {
            var book = await _comicBookService.GetComicBookByIdAsync(id);
            if (book == null)
            {
                return Json(new { success = false, price = 0 });
            }
            return Json(new { success = true, price = book.PricePerDay, title = book.Title });
        }

        private async Task PopulateDropDownsAsync(RentalCreateViewModel model)
        {
            var customers = await _customerService.GetAllCustomersAsync();
            var books = await _comicBookService.GetAllComicBooksAsync();

            model.CustomerList = customers.Select(c => new SelectListItem
            {
                Value = c.CustomerID.ToString(),
                Text = $"{c.FullName} ({c.PhoneNumber})"
            });

            model.ComicBookList = books.Select(b => new SelectListItem
            {
                Value = b.ComicBookID.ToString(),
                Text = $"{b.Title} - {b.PricePerDay:N0} đ/ngày"
            });

            // If a book is already selected, populate its PricePerDay
            if (model.ComicBookID > 0 && model.PricePerDay <= 0)
            {
                var selectedBook = books.FirstOrDefault(b => b.ComicBookID == model.ComicBookID);
                if (selectedBook != null)
                {
                    model.PricePerDay = selectedBook.PricePerDay;
                }
            }
            else if (model.ComicBookID == 0 && books.Any())
            {
                var first = books.First();
                model.ComicBookID = first.ComicBookID;
                model.PricePerDay = first.PricePerDay;
            }
        }
    }
}
