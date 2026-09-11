using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Web.Data;
using ComicSystem.Web.Models;
using ComicSystem.Web.Models.ViewModels;

namespace ComicSystem.Web.Controllers;

public class RentalsController : Controller
{
    private readonly ComicSystemDbContext _context;

    public RentalsController(ComicSystemDbContext context)
    {
        _context = context;
    }

    // GET: Rentals
    public async Task<IActionResult> Index(string? statusFilter)
    {
        ViewData["StatusFilter"] = statusFilter;

        var query = _context.Rentals
            .Include(r => r.Customer)
            .Include(r => r.RentalDetails)
                .ThenInclude(rd => rd.ComicBook)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(statusFilter))
        {
            query = query.Where(r => r.Status == statusFilter);
        }

        var rentals = await query.OrderByDescending(r => r.RentalDate).ToListAsync();
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
            .FirstOrDefaultAsync(m => m.RentalID == id);

        if (rental == null) return NotFound();

        return View(rental);
    }

    // GET: Rentals/Create (Rental book page - Question 3)
    public async Task<IActionResult> Create(int? customerId)
    {
        var model = new RentalCreateViewModel
        {
            RentalDate = DateTime.Today,
            ReturnDate = DateTime.Today.AddDays(7),
            Quantity = 1,
            CustomerID = customerId ?? 0
        };

        await PopulateDropdownsAsync(model);
        return View(model);
    }

    // POST: Rentals/Create (Insert into Rentals and RentalDetails tables)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RentalCreateViewModel model)
    {
        if (model.ReturnDate < model.RentalDate)
        {
            ModelState.AddModelError(nameof(model.ReturnDate), "Ngày trả phải sau hoặc bằng ngày thuê.");
        }

        var book = await _context.ComicBooks.FindAsync(model.ComicBookID);
        if (book == null)
        {
            ModelState.AddModelError(nameof(model.ComicBookID), "Truyện được chọn không tồn tại.");
        }

        var customer = await _context.Customers.FindAsync(model.CustomerID);
        if (customer == null)
        {
            ModelState.AddModelError(nameof(model.CustomerID), "Khách hàng được chọn không tồn tại.");
        }

        if (ModelState.IsValid && book != null && customer != null)
        {
            // Execute in transaction to ensure atomic insert into Rentals and RentalDetails
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Insert into Rentals
                var rental = new Rental
                {
                    CustomerID = model.CustomerID,
                    RentalDate = model.RentalDate,
                    ReturnDate = model.ReturnDate,
                    Status = string.IsNullOrWhiteSpace(model.Status) ? "Đang thuê" : model.Status
                };
                _context.Rentals.Add(rental);
                await _context.SaveChangesAsync();

                // 2. Insert into RentalDetails
                var rentalDetail = new RentalDetail
                {
                    RentalID = rental.RentalID,
                    ComicBookID = model.ComicBookID,
                    Quantity = model.Quantity,
                    PricePerDay = model.PricePerDay > 0 ? model.PricePerDay : book.PricePerDay
                };
                _context.RentalDetails.Add(rentalDetail);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                TempData["SuccessMessage"] = $"Tạo phiếu thuê #{rental.RentalID} cho khách hàng '{customer.FullName}' thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, $"Lỗi khi lưu dữ liệu thuê sách: {ex.Message}");
            }
        }

        await PopulateDropdownsAsync(model);
        return View(model);
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
            TempData["SuccessMessage"] = $"Đã cập nhật trạng thái phiếu thuê #{id} thành 'Đã trả'!";
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Rentals/GetBookPrice/5 (AJAX API helper)
    [HttpGet]
    public async Task<IActionResult> GetBookPrice(int id)
    {
        var book = await _context.ComicBooks.FindAsync(id);
        if (book == null) return NotFound();
        return Json(new { pricePerDay = book.PricePerDay, title = book.Title });
    }

    private async Task PopulateDropdownsAsync(RentalCreateViewModel model)
    {
        var customers = await _context.Customers
            .OrderBy(c => c.FullName)
            .Select(c => new SelectListItem
            {
                Value = c.CustomerID.ToString(),
                Text = $"{c.FullName} ({c.PhoneNumber})"
            })
            .ToListAsync();

        var books = await _context.ComicBooks
            .OrderBy(b => b.Title)
            .Select(b => new SelectListItem
            {
                Value = b.ComicBookID.ToString(),
                Text = $"{b.Title} - {b.PricePerDay:N0} đ/ngày"
            })
            .ToListAsync();

        model.CustomerList = customers;
        model.ComicBookList = books;
    }
}
