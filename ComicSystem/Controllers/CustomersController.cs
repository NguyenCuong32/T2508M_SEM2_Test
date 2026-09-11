using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComicSystem.Services.Interfaces;
using ComicSystem.ViewModels;

namespace ComicSystem.Controllers
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
            return View(new CustomerRegisterViewModel
            {
                RegistrationDate = DateTime.Today
            });
        }

        // POST: Customers/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CustomerRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (success, message, customer) = await _customerService.RegisterCustomerAsync(model);
                if (success)
                {
                    TempData["SuccessMessage"] = message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(nameof(model.PhoneNumber), message);
            }
            return View(model);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var customer = await _customerService.GetCustomerWithRentalsAsync(id.Value);
            if (customer == null) return NotFound();

            return View(customer);
        }
    }
}
