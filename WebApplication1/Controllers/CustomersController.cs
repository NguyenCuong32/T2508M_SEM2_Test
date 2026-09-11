using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return View(customers);
        }

        // GET: Customers/Register
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
        public async Task<IActionResult> Register([Bind("FullName,PhoneNumber,RegistrationDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                // Check if phone number already registered
                var existing = await _customerService.GetCustomerByPhoneAsync(customer.PhoneNumber);
                if (existing != null)
                {
                    ModelState.AddModelError("PhoneNumber", "Số điện thoại này đã được đăng ký trước đó.");
                    return View(customer);
                }

                if (customer.RegistrationDate == default)
                {
                    customer.RegistrationDate = DateTime.Now;
                }

                await _customerService.RegisterCustomerAsync(customer);
                TempData["SuccessMessage"] = $"Đăng ký khách hàng '{customer.FullName}' thành công! Khách hàng có thể bắt đầu thuê truyện.";
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
    }
}
