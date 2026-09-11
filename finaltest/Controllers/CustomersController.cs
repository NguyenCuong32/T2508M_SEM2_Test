using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using finaltest.Models;

namespace finaltest.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ComicDbContext _context;

        public CustomersController(ComicDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string searchString)
        {
            var query = _context.Customers
                .Include(c => c.Rentals)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(c => c.FullName.Contains(searchString) || c.PhoneNumber.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;
            var customers = await query.OrderByDescending(c => c.RegistrationDate).ToListAsync();
            return View(customers);
        }

        // GET: Customers/Register or Customers/Create
        public IActionResult Register()
        {
            var customer = new Customer
            {
                RegistrationDate = DateTime.Now
            };
            return View(customer);
        }

        // POST: Customers/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("CustomerID,FullName,PhoneNumber,RegistrationDate")] Customer customer)
        {
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
}
