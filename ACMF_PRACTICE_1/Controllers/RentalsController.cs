using ACMF_PRACTICE_1.Data;
using ACMF_PRACTICE_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ACMF_PRACTICE_1.Controllers
{
    public class RentalsController : Controller
    {
        private readonly AppDbContext _context;

        public RentalsController(AppDbContext context)
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

        // GET: Rentals/Create
        public IActionResult Create()
        {
            PopulateDropDowns();
            return View(new RentalCreateViewModel());
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentalCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // Validate return date is after rental date
                if (vm.ReturnDate <= vm.RentalDate)
                {
                    ModelState.AddModelError("ReturnDate", "Return date must be after rental date.");
                    PopulateDropDowns();
                    return View(vm);
                }

                // Fetch PricePerDay from selected ComicBook
                var book = await _context.ComicBooks.FindAsync(vm.ComicBookID);
                if (book == null)
                {
                    ModelState.AddModelError("ComicBookID", "Selected comic book not found.");
                    PopulateDropDowns();
                    return View(vm);
                }

                var rental = new Rental
                {
                    CustomerID = vm.CustomerID,
                    RentalDate = vm.RentalDate,
                    ReturnDate = vm.ReturnDate,
                    Status = vm.Status
                };

                _context.Rentals.Add(rental);
                await _context.SaveChangesAsync();

                var detail = new RentalDetail
                {
                    RentalID = rental.RentalID,
                    ComicBookID = vm.ComicBookID,
                    Quantity = vm.Quantity,
                    PricePerDay = book.PricePerDay
                };

                _context.RentalDetails.Add(detail);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Rental created successfully.";
                return RedirectToAction(nameof(Index));
            }

            PopulateDropDowns();
            return View(vm);
        }

        // GET: Rentals/AddBook/5  (add another book to existing rental)
        public async Task<IActionResult> AddBook(int? id)
        {
            if (id == null) return NotFound();

            var rental = await _context.Rentals
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.RentalID == id);

            if (rental == null) return NotFound();

            ViewBag.Rental = rental;
            ViewBag.Books = new SelectList(_context.ComicBooks, "ComicBookID", "Title");
            return View();
        }

        // POST: Rentals/AddBook/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook(int id, int comicBookID, int quantity)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null) return NotFound();

            var book = await _context.ComicBooks.FindAsync(comicBookID);
            if (book == null)
            {
                TempData["Error"] = "Comic book not found.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var detail = new RentalDetail
            {
                RentalID = id,
                ComicBookID = comicBookID,
                Quantity = quantity,
                PricePerDay = book.PricePerDay
            };

            _context.RentalDetails.Add(detail);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"\"{book.Title}\" added to rental.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // POST: Rentals/UpdateStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null) return NotFound();

            rental.Status = status;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Rental status updated.";
            return RedirectToAction(nameof(Details), new { id });
        }

        private void PopulateDropDowns()
        {
            ViewBag.Customers = new SelectList(_context.Customers, "CustomerID", "FullName");
            ViewBag.Books = new SelectList(_context.ComicBooks, "ComicBookID", "Title");
            ViewBag.StatusOptions = new SelectList(new[] { "Đang thuê", "Có thể thuê" });
        }
    }
}
