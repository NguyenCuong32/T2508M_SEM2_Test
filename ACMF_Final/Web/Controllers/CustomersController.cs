using ACMF_Final.Application.Interfaces;
using ACMF_Final.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ACMF_Final.Web.Controllers
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
            var customers = await _customerService.GetAllAsync();
            return View(customers);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerService.AddAsync(customer);
                TempData["SuccessMessage"] = "Customer registered successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
    }
}
