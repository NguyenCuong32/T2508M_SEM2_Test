using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using finaltest.Models;

namespace finaltest.Controllers
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
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .OrderByDescending(r => r.RentalDate)
                .ThenByDescending(r => r.RentalID)
                .ToListAsync();

            return View(rentals);
        }

        // GET: Rentals/Create
        public async Task<IActionResult> Create(int? customerId, int? comicBookId)
        {
            var customers = await _context.Customers.OrderBy(c => c.FullName).ToListAsync();
            var comicBooks = await _context.ComicBooks.OrderBy(b => b.Title).ToListAsync();

            var viewModel = new RentalCreateViewModel
            {
                RentalDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddDays(7),
                Quantity = 1,
                Status = "Đang thuê",
                CustomersList = new SelectList(customers, "CustomerID", "FullName", customerId),
                ComicBooksList = new SelectList(comicBooks.Select(b => new { 
                    b.ComicBookID, 
                    DisplayText = $"{b.Title} - {b.Author} ({b.PricePerDay:N0} đ/ngày)" 
                }), "ComicBookID", "DisplayText", comicBookId)
            };

            if (comicBookId.HasValue)
            {
                var book = await _context.ComicBooks.FindAsync(comicBookId.Value);
                if (book != null)
                {
                    viewModel.PricePerDay = book.PricePerDay;
                }
            }
            else if (comicBooks.Any())
            {
                viewModel.PricePerDay = comicBooks.First().PricePerDay;
            }

            return View(viewModel);
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentalCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Fetch book for default price check if not set
                var book = await _context.ComicBooks.FindAsync(viewModel.ComicBookID);
                if (book == null)
                {
                    ModelState.AddModelError("ComicBookID", "Truyện tranh không tồn tại.");
                }
                else
                {
                    if (viewModel.PricePerDay <= 0)
                    {
                        viewModel.PricePerDay = book.PricePerDay;
                    }

                    // 1. Create Rental record
                    var rental = new Rental
                    {
                        CustomerID = viewModel.CustomerID,
                        RentalDate = viewModel.RentalDate,
                        ReturnDate = viewModel.ReturnDate,
                        Status = string.IsNullOrWhiteSpace(viewModel.Status) ? "Đang thuê" : viewModel.Status
                    };

                    _context.Rentals.Add(rental);
                    await _context.SaveChangesAsync();

                    // 2. Create RentalDetail record
                    var rentalDetail = new RentalDetail
                    {
                        RentalID = rental.RentalID,
                        ComicBookID = viewModel.ComicBookID,
                        Quantity = viewModel.Quantity,
                        PricePerDay = viewModel.PricePerDay
                    };

                    _context.RentalDetails.Add(rentalDetail);
                    await _context.SaveChangesAsync();

                    var customer = await _context.Customers.FindAsync(viewModel.CustomerID);
                    TempData["SuccessMessage"] = $"Tạo phiếu thuê thành công cho khách hàng '{customer?.FullName ?? "N/A"}' - Truyện '{book.Title}'!";
                    return RedirectToAction(nameof(Index));
                }
            }

            // Repopulate select lists on validation failure
            var customers = await _context.Customers.OrderBy(c => c.FullName).ToListAsync();
            var comicBooks = await _context.ComicBooks.OrderBy(b => b.Title).ToListAsync();
            viewModel.CustomersList = new SelectList(customers, "CustomerID", "FullName", viewModel.CustomerID);
            viewModel.ComicBooksList = new SelectList(comicBooks.Select(b => new { 
                b.ComicBookID, 
                DisplayText = $"{b.Title} - {b.Author} ({b.PricePerDay:N0} đ/ngày)" 
            }), "ComicBookID", "DisplayText", viewModel.ComicBookID);

            return View(viewModel);
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var rental = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .FirstOrDefaultAsync(m => m.RentalID == id);

            if (rental == null) return NotFound();

            return View(rental);
        }

        // POST: Rentals/ReturnBook/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental != null)
            {
                rental.Status = "Đã trả";
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã cập nhật trạng thái phiếu thuê #{rental.RentalID} thành 'Đã trả'!";
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Rentals/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã xóa phiếu thuê #{id} thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
