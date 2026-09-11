using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Services;

namespace MyMvcApp.Controllers;

public class CustomersController(ComicService service) : Controller
{
    public async Task<IActionResult> Index() => View(await service.GetCustomersAsync());

    public IActionResult Create() => View(new Customer());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FullName,PhoneNumber,RegistrationDate")] Customer customer)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await service.RegisterCustomerAsync(customer);
                TempData["Success"] = "Đã đăng ký khách hàng.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex) { ModelState.AddModelError("", ex.Message); }
        }
        return View(customer);
    }
}
