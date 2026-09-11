using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;
using ComicSystem.ViewModels;

namespace ComicSystem.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ComicDbContext _context;

        public RentalsController(ComicDbContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var rentals = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails!)
                    .ThenInclude(rd => rd.ComicBook)
                .OrderByDescending(r => r.RentalID)
                .ToListAsync();

            return View(rentals);
        }

        // GET: Rentals/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CustomerID"] = new SelectList(await _context.Customers.ToListAsync(), "CustomerID", "FullName");
            ViewData["ComicBookID"] = new SelectList(await _context.ComicBooks.ToListAsync(), "ComicBookID", "Title");

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
                // Find book to get default PricePerDay if not provided
                var comicBook = await _context.ComicBooks.FindAsync(model.ComicBookID);
                if (comicBook == null)
                {
                    ModelState.AddModelError("ComicBookID", "Truyện được chọn không tồn tại.");
                }
                else
                {
                    decimal price = model.PricePerDay > 0 ? model.PricePerDay : comicBook.PricePerDay;

                    // 1. Insert into Rentals table
                    var rental = new Rental
                    {
                        CustomerID = model.CustomerID,
                        RentalDate = model.RentalDate,
                        ReturnDate = model.ReturnDate,
                        Status = "Đang thuê"
                    };

                    _context.Rentals.Add(rental);
                    await _context.SaveChangesAsync();

                    // 2. Insert into RentalDetails table
                    var rentalDetail = new RentalDetail
                    {
                        RentalID = rental.RentalID,
                        ComicBookID = model.ComicBookID,
                        Quantity = model.Quantity,
                        PricePerDay = price
                    };

                    _context.RentalDetails.Add(rentalDetail);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Cho thuê truyện thành công!";
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["CustomerID"] = new SelectList(await _context.Customers.ToListAsync(), "CustomerID", "FullName", model.CustomerID);
            ViewData["ComicBookID"] = new SelectList(await _context.ComicBooks.ToListAsync(), "ComicBookID", "Title", model.ComicBookID);
            return View(model);
        }

        // API Endpoint for fetching ComicBook price via AJAX (Bonus UI feature)
        [HttpGet]
        public async Task<IActionResult> GetBookPrice(int id)
        {
            var book = await _context.ComicBooks.FindAsync(id);
            if (book == null) return NotFound();
            return Json(new { price = book.PricePerDay });
        }
    }
}
