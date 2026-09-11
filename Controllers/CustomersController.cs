using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComicSystem.Models;
using ComicSystem.Repositories;

namespace ComicSystem.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customers = await _customerRepository.GetAllAsync();
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
                await _customerRepository.AddAsync(customer);
                TempData["SuccessMessage"] = $"Đăng ký thành công cho khách hàng: {customer.FullName}!";
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
    }
}
