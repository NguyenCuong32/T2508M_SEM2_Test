using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ComicSystem.Models;
using ComicSystem.Repositories;
using ComicSystem.ViewModels;

namespace ComicSystem.Controllers
{
    public class RentalsController : Controller
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IComicBookRepository _comicBookRepository;

        public RentalsController(
            IRentalRepository rentalRepository,
            ICustomerRepository customerRepository,
            IComicBookRepository comicBookRepository)
        {
            _rentalRepository = rentalRepository;
            _customerRepository = customerRepository;
            _comicBookRepository = comicBookRepository;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var rentals = await _rentalRepository.GetAllAsync();
            return View(rentals);
        }

        // GET: Rentals/Create
        public async Task<IActionResult> Create()
        {
            var customers = await _customerRepository.GetAllAsync();
            var comicBooks = await _comicBookRepository.GetAllAsync();

            ViewData["CustomerID"] = new SelectList(customers, "CustomerID", "FullName");
            ViewData["ComicBookID"] = new SelectList(comicBooks, "ComicBookID", "Title");

            var viewModel = new RentalCreateViewModel
            {
                RentalDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddDays(7),
                Quantity = 1
            };

            return View(viewModel);
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentalCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var comicBook = await _comicBookRepository.GetByIdAsync(model.ComicBookID);
                if (comicBook == null)
                {
                    ModelState.AddModelError("ComicBookID", "Truyện được chọn không tồn tại.");
                }
                else
                {
                    decimal price = model.PricePerDay > 0 ? model.PricePerDay : comicBook.PricePerDay;

                    var rental = new Rental
                    {
                        CustomerID = model.CustomerID,
                        RentalDate = model.RentalDate,
                        ReturnDate = model.ReturnDate,
                        Status = "Đang thuê"
                    };

                    var rentalDetail = new RentalDetail
                    {
                        ComicBookID = model.ComicBookID,
                        Quantity = model.Quantity,
                        PricePerDay = price
                    };

                    await _rentalRepository.AddRentalWithDetailAsync(rental, rentalDetail);

                    TempData["SuccessMessage"] = "Cho thuê truyện thành công!";
                    return RedirectToAction(nameof(Index));
                }
            }

            var customers = await _customerRepository.GetAllAsync();
            var comicBooks = await _comicBookRepository.GetAllAsync();

            ViewData["CustomerID"] = new SelectList(customers, "CustomerID", "FullName", model.CustomerID);
            ViewData["ComicBookID"] = new SelectList(comicBooks, "ComicBookID", "Title", model.ComicBookID);
            return View(model);
        }

        // API Endpoint for fetching ComicBook price via AJAX
        [HttpGet]
        public async Task<IActionResult> GetBookPrice(int id)
        {
            var book = await _comicBookRepository.GetByIdAsync(id);
            if (book == null) return NotFound();
            return Json(new { price = book.PricePerDay });
        }
    }
}
