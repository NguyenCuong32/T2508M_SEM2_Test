using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using finaltest_ACMF.Models;

namespace finaltest_ACMF.Controllers
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
                .ToListAsync();

            return View(rentals);
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .FirstOrDefaultAsync(m => m.RentalID == id);

            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create (Question 3)
        public async Task<IActionResult> Create(int? customerId, int? comicBookId)
        {
            await PopulateDropDownsAsync(customerId, comicBookId);

            var model = new RentalCreateViewModel
            {
                CustomerID = customerId ?? 0,
                ComicBookID = comicBookId ?? 0,
                RentalDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddDays(7),
                Quantity = 1,
                Status = "Đang thuê"
            };

            if (comicBookId.HasValue && comicBookId.Value > 0)
            {
                var book = await _context.ComicBooks.FindAsync(comicBookId.Value);
                if (book != null)
                {
                    model.PricePerDay = book.PricePerDay;
                }
            }

            return View(model);
        }

        // POST: Rentals/Create (Question 3)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentalCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = await _context.ComicBooks.FindAsync(model.ComicBookID);
                if (book == null)
                {
                    ModelState.AddModelError("ComicBookID", "Truyện không tồn tại.");
                    await PopulateDropDownsAsync(model.CustomerID, model.ComicBookID);
                    return View(model);
                }

                var customer = await _context.Customers.FindAsync(model.CustomerID);
                if (customer == null)
                {
                    ModelState.AddModelError("CustomerID", "Khách hàng không tồn tại.");
                    await PopulateDropDownsAsync(model.CustomerID, model.ComicBookID);
                    return View(model);
                }

                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // 1. Create Rental Record
                    var rental = new Rental
                    {
                        CustomerID = model.CustomerID,
                        RentalDate = model.RentalDate,
                        ReturnDate = model.ReturnDate,
                        Status = string.IsNullOrWhiteSpace(model.Status) ? "Đang thuê" : model.Status
                    };
                    _context.Rentals.Add(rental);
                    await _context.SaveChangesAsync();

                    // 2. Create RentalDetail Record
                    var rentalDetail = new RentalDetail
                    {
                        RentalID = rental.RentalID,
                        ComicBookID = model.ComicBookID,
                        Quantity = model.Quantity,
                        PricePerDay = model.PricePerDay ?? book.PricePerDay
                    };
                    _context.RentalDetails.Add(rentalDetail);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    TempData["SuccessMessage"] = $"Lập phiếu thuê #{rental.RentalID} cho khách '{customer.FullName}' thành công!";
                    return RedirectToAction(nameof(Details), new { id = rental.RentalID });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi tạo phiếu thuê: " + ex.Message);
                }
            }

            await PopulateDropDownsAsync(model.CustomerID, model.ComicBookID);
            return View(model);
        }

        // POST: Rentals/ReturnRental/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental != null)
            {
                rental.Status = "Đã trả";
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã cập nhật trạng thái phiếu thuê #{id} thành 'Đã trả'.";
            }
            return RedirectToAction(nameof(Details), new { id });
        }

        // API Endpoint for frontend price lookup
        [HttpGet]
        public async Task<IActionResult> GetBookPrice(int id)
        {
            var book = await _context.ComicBooks.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Json(new { price = book.PricePerDay, title = book.Title, author = book.Author });
        }

        private async Task PopulateDropDownsAsync(int? selectedCustomerId = null, int? selectedBookId = null)
        {
            var customers = await _context.Customers
                .OrderBy(c => c.FullName)
                .Select(c => new { c.CustomerID, DisplayText = $"{c.FullName} ({c.PhoneNumber})" })
                .ToListAsync();

            var books = await _context.ComicBooks
                .OrderBy(b => b.Title)
                .Select(b => new { b.ComicBookID, DisplayText = $"{b.Title} - {b.PricePerDay:N0} VNĐ/ngày" })
                .ToListAsync();

            ViewBag.CustomerID = new SelectList(customers, "CustomerID", "DisplayText", selectedCustomerId);
            ViewBag.ComicBookID = new SelectList(books, "ComicBookID", "DisplayText", selectedBookId);
        }
    }
}
