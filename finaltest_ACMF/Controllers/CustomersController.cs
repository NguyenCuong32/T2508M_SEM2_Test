using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using finaltest_ACMF.Models;

namespace finaltest_ACMF.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ComicDbContext _context;

        public CustomersController(ComicDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string? searchString)
        {
            var query = _context.Customers.Include(c => c.Rentals).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(c => c.FullName.Contains(searchString) || c.PhoneNumber.Contains(searchString));
                ViewData["CurrentFilter"] = searchString;
            }

            var list = await query.OrderByDescending(c => c.RegistrationDate).ToListAsync();
            return View(list);
        }

        // GET: Customers/Register (Question 2)
        public IActionResult Register()
        {
            var model = new Customer
            {
                RegistrationDate = DateTime.Now
            };
            return View(model);
        }

        // POST: Customers/Register (Question 2)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("CustomerID,FullName,PhoneNumber,RegistrationDate")] Customer customer)
        {
            // Check if phone number is already registered
            if (await _context.Customers.AnyAsync(c => c.PhoneNumber == customer.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "Số điện thoại này đã được đăng ký trong hệ thống.");
            }

            if (ModelState.IsValid)
            {
                if (customer.RegistrationDate == default)
                {
                    customer.RegistrationDate = DateTime.Now;
                }

                _context.Add(customer);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đăng ký thành viên cho '{customer.FullName}' thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // Alias for Create
        public IActionResult Create() => RedirectToAction(nameof(Register));

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Rentals)
                    .ThenInclude(r => r.RentalDetails)
                        .ThenInclude(rd => rd.ComicBook)
                .FirstOrDefaultAsync(m => m.CustomerID == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,FullName,PhoneNumber,RegistrationDate")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Cập nhật thông tin khách hàng '{customer.FullName}' thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Customers.Any(e => e.CustomerID == customer.CustomerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Rentals)
                .FirstOrDefaultAsync(m => m.CustomerID == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã xóa khách hàng '{customer.FullName}' thành công!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
