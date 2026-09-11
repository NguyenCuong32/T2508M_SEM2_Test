using Microsoft.AspNetCore.Mvc;
using T2508M_SEM_Test.DTOs.Customers;
using T2508M_SEM_Test.Services.Interfaces;

namespace T2508M_SEM_Test.Controllers;

public class CustomersController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    // GET: /Customers
    public async Task<IActionResult> Index()
    {
        var customers = await _customerService.GetAllAsync();

        return View(customers);
    }

    // GET: /Customers/Create
    public IActionResult Create()
    {
        var dto = new CustomerCreateDto
        {
            RegistrationDate = DateTime.Now
        };

        return View(dto);
    }

    // POST: /Customers/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CustomerCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var result = await _customerService.CreateAsync(dto);

        if (!result.Success)
        {
            ModelState.AddModelError(
                nameof(dto.PhoneNumber),
                result.ErrorMessage!);

            return View(dto);
        }

        TempData["SuccessMessage"] =
            "Customer registered successfully.";

        return RedirectToAction(nameof(Index));
    }
}