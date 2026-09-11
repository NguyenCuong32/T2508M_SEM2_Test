using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;

namespace ComicSystem.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ComicSystemContext _context;

        public RentalsController(ComicSystemContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var rentals = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .OrderByDescending(r => r.RentalDate)
                .ToListAsync();
            return View(rentals);
        }

        // GET: Rentals/Create
        public async Task<IActionResult> Create()
        {
            var model = new RentalViewModel
            {
                RentalDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddDays(7),
                Quantity = 1,
                Customers = new SelectList(await _context.Customers.ToListAsync(), "CustomerID", "FullName"),
                ComicBooks = new SelectList(await _context.ComicBooks.ToListAsync(), "ComicBookID", "Title")
            };
            return View(model);
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentalViewModel model)
        {
            if (ModelState.IsValid)
            {
                var comicBook = await _context.ComicBooks.FindAsync(model.ComicBookID);
                if (comicBook == null)
                {
                    ModelState.AddModelError("", "Comic book not found.");
                    model.Customers = new SelectList(await _context.Customers.ToListAsync(), "CustomerID", "FullName", model.CustomerID);
                    model.ComicBooks = new SelectList(await _context.ComicBooks.ToListAsync(), "ComicBookID", "Title", model.ComicBookID);
                    return View(model);
                }

                if (model.ReturnDate <= model.RentalDate)
                {
                    ModelState.AddModelError("ReturnDate", "Return date must be after rental date.");
                    model.Customers = new SelectList(await _context.Customers.ToListAsync(), "CustomerID", "FullName", model.CustomerID);
                    model.ComicBooks = new SelectList(await _context.ComicBooks.ToListAsync(), "ComicBookID", "Title", model.ComicBookID);
                    return View(model);
                }

                // Create Rental
                var rental = new Rental
                {
                    CustomerID = model.CustomerID,
                    RentalDate = model.RentalDate,
                    ReturnDate = model.ReturnDate,
                    Status = "Active"
                };

                _context.Rentals.Add(rental);
                await _context.SaveChangesAsync();

                // Create RentalDetail
                var rentalDetail = new RentalDetail
                {
                    RentalID = rental.RentalID,
                    ComicBookID = model.ComicBookID,
                    Quantity = model.Quantity,
                    PricePerDay = comicBook.PricePerDay
                };

                _context.RentalDetails.Add(rentalDetail);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Book rented successfully!";
                return RedirectToAction(nameof(Index));
            }

            model.Customers = new SelectList(await _context.Customers.ToListAsync(), "CustomerID", "FullName", model.CustomerID);
            model.ComicBooks = new SelectList(await _context.ComicBooks.ToListAsync(), "ComicBookID", "Title", model.ComicBookID);
            return View(model);
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var rental = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .FirstOrDefaultAsync(r => r.RentalID == id);

            if (rental == null) return NotFound();

            return View(rental);
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var rental = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .FirstOrDefaultAsync(r => r.RentalID == id);

            if (rental == null) return NotFound();

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rentals
                .Include(r => r.RentalDetails)
                .FirstOrDefaultAsync(r => r.RentalID == id);

            if (rental != null)
            {
                _context.RentalDetails.RemoveRange(rental.RentalDetails);
                _context.Rentals.Remove(rental);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Rental deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
