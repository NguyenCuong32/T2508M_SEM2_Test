using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;

namespace ComicSystem.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ComicDbContext _context;

        public CustomersController(ComicDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync();
            return View(customers);
        }

        // GET: Customers/Register
        public IActionResult Register()
        {
            var customer = new Customer
            {
                RegisterDate = DateTime.Today
            };
            return View(customer);
        }

        // POST: Customers/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("CustomerID,FullName,PhoneNumber,RegisterDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đăng ký thành công cho khách hàng: {customer.FullName}!";
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
    }
}
