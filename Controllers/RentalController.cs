using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    public class RentalController : Controller
    {
        private readonly BookStoreContext _context;
        public RentalController(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var rentals = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                .ThenInclude(d => d.ComicBook)
                .ToListAsync();
            return View(rentals);
        }

        public IActionResult Create()
        {
            ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "FullName");
            ViewBag.ComicBooks = new SelectList(_context.ComicBooks, "ComicBookId", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rental rental, int ComicBookId, int CustomerId)
        {
            if (Quantity <= 0)
            {
                ModelState.AddModelError("Quantity", "Quantity must be greater than zero.");
            }
            
            var book = await _context.ComicBooks.FindAsync(ComicBookId);
            if (book == null)
            {
                ModelState.AddModelError("ComicBookId", "Selected comic book does not exist.");
            }
            if (!ModelState.IsValid)
            {
                rental.RentalDate = DateTime.Now;
                rental.Status = "Pending";
                _context.Rentals.Add(rental);
                await _context.SaveChangesAsync();
                var detail = new RentalDetail
                {
                    RentalId = rental.RentalId,
                    ComicBookId = ComicBookId,
                    Quantity = Quantity,
                    PricePerDay = book.PricePerDay
                };
                _context.RentalDetails.Add(detail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "FullName");
            ViewBag.ComicBooks = new SelectList(_context.ComicBooks, "ComicBookId", "Title");
            return View(rental);
        }
    }
}
