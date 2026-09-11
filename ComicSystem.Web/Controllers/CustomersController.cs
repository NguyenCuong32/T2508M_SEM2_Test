using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Web.Data;
using ComicSystem.Web.Models;

namespace ComicSystem.Web.Controllers;

public class CustomersController : Controller
{
    private readonly ComicSystemDbContext _context;

    public CustomersController(ComicSystemDbContext context)
    {
        _context = context;
    }

    // GET: Customers
    public async Task<IActionResult> Index(string? searchString)
    {
        ViewData["CurrentFilter"] = searchString;

        var query = _context.Customers
            .Include(c => c.Rentals)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            var search = searchString.Trim();
            query = query.Where(c => 
                c.FullName.Contains(search) || 
                c.PhoneNumber.Contains(search));
        }

        var customers = await query.OrderByDescending(c => c.RegistrationDate).ToListAsync();
        return View(customers);
    }

    // GET: Customers/Register
    public IActionResult Register()
    {
        var model = new Customer
        {
            RegistrationDate = DateTime.Now
        };
        return View(model);
    }

    // POST: Customers/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([Bind("FullName,PhoneNumber,RegistrationDate")] Customer customer)
    {
        // Check if phone number already exists
        if (await _context.Customers.AnyAsync(c => c.PhoneNumber == customer.PhoneNumber))
        {
            ModelState.AddModelError("PhoneNumber", "Số điện thoại này đã được đăng ký trong hệ thống.");
        }

        if (ModelState.IsValid)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Đăng ký khách hàng '{customer.FullName}' thành công!";
            return RedirectToAction(nameof(Index));
        }

        return View(customer);
    }

    // GET: Customers/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var customer = await _context.Customers
            .Include(c => c.Rentals)
                .ThenInclude(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
            .FirstOrDefaultAsync(m => m.CustomerID == id);

        if (customer == null) return NotFound();

        return View(customer);
    }
}
