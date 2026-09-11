using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;
using ComicSystem.Models.ViewModels;

namespace ComicSystem.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ComicSystemDbContext _context;

        public RentalsController(ComicSystemDbContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index(string? statusFilter, string? searchString)
        {
            ViewData["CurrentStatus"] = statusFilter;
            ViewData["CurrentFilter"] = searchString;

            var rentalsQuery = _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .AsQueryable();

            if (!string.IsNullOrEmpty(statusFilter))
            {
                rentalsQuery = rentalsQuery.Where(r => r.Status == statusFilter);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                rentalsQuery = rentalsQuery.Where(r => r.Customer!.FullName.Contains(searchString) 
                                                    || r.Customer!.PhoneNumber.Contains(searchString)
                                                    || r.RentalDetails.Any(rd => rd.ComicBook!.Title.Contains(searchString)));
            }

            var rentals = await rentalsQuery.OrderByDescending(r => r.RentalDate).ToListAsync();
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

        // GET: Rentals/Create
        public async Task<IActionResult> Create(int? customerId, int? comicBookId)
        {
            var customers = await _context.Customers.OrderBy(c => c.FullName).ToListAsync();
            var comicBooks = await _context.ComicBooks.OrderBy(b => b.Title).ToListAsync();

            if (!customers.Any())
            {
                TempData["ErrorMessage"] = "Chưa có khách hàng nào trong hệ thống. Vui lòng đăng ký khách hàng trước khi thuê sách!";
                return RedirectToAction("Register", "Customers");
            }

            if (!comicBooks.Any())
            {
                TempData["ErrorMessage"] = "Chưa có truyện tranh nào trong kho. Vui lòng thêm truyện trước!";
                return RedirectToAction("Create", "ComicBooks");
            }

            var selectedBook = comicBookId.HasValue 
                ? comicBooks.FirstOrDefault(b => b.ComicBookID == comicBookId.Value) 
                : comicBooks.First();

            var viewModel = new RentalCreateViewModel
            {
                CustomerID = customerId ?? customers.First().CustomerID,
                ComicBookID = selectedBook?.ComicBookID ?? comicBooks.First().ComicBookID,
                RentalDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddDays(7),
                Quantity = 1,
                PricePerDay = selectedBook?.PricePerDay ?? 5000m,
                Status = "Đang thuê",
                CustomersList = new SelectList(customers, "CustomerID", "FullName", customerId),
                ComicBooksList = new SelectList(comicBooks, "ComicBookID", "Title", selectedBook?.ComicBookID)
            };

            return View(viewModel);
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentalCreateViewModel model)
        {
            if (model.ReturnDate < model.RentalDate)
            {
                ModelState.AddModelError("ReturnDate", "Ngày trả không được trước ngày thuê.");
            }

            if (ModelState.IsValid)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // 1. Insert into Rentals table
                    var rental = new Rental
                    {
                        CustomerID = model.CustomerID,
                        RentalDate = model.RentalDate,
                        ReturnDate = model.ReturnDate,
                        Status = string.IsNullOrWhiteSpace(model.Status) ? "Đang thuê" : model.Status
                    };

                    _context.Rentals.Add(rental);
                    await _context.SaveChangesAsync();

                    // 2. Insert into RentalDetails table
                    var rentalDetail = new RentalDetail
                    {
                        RentalID = rental.RentalID,
                        ComicBookID = model.ComicBookID,
                        Quantity = model.Quantity,
                        PricePerDay = model.PricePerDay
                    };

                    _context.RentalDetails.Add(rentalDetail);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    var customer = await _context.Customers.FindAsync(model.CustomerID);
                    var book = await _context.ComicBooks.FindAsync(model.ComicBookID);

                    TempData["SuccessMessage"] = $"Đã tạo phiếu thuê thành công cho khách hàng {customer?.FullName} (Truyện: {book?.Title}, SL: {model.Quantity})!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "Đã có lỗi xảy ra khi lưu phiếu thuê: " + ex.Message);
                }
            }

            // Reload dropdown lists if validation fails
            var customers = await _context.Customers.OrderBy(c => c.FullName).ToListAsync();
            var comicBooks = await _context.ComicBooks.OrderBy(b => b.Title).ToListAsync();
            model.CustomersList = new SelectList(customers, "CustomerID", "FullName", model.CustomerID);
            model.ComicBooksList = new SelectList(comicBooks, "ComicBookID", "Title", model.ComicBookID);

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
                TempData["SuccessMessage"] = $"Phiếu thuê #{id} đã được chuyển sang trạng thái ĐÃ TRẢ!";
            }
            return RedirectToAction(nameof(Index));
        }

        // AJAX GET: Rentals/GetBookPrice/5
        [HttpGet]
        public async Task<IActionResult> GetBookPrice(int id)
        {
            var book = await _context.ComicBooks.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Json(new { price = book.PricePerDay });
        }
    }
}
