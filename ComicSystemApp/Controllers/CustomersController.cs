using Microsoft.AspNetCore.Mvc;
using ComicSystemApp.Data;
using ComicSystemApp.Models;
using ComicSystemApp.DTOs;

public class CustomersController : Controller
{
    private readonly ApplicationDbContext _db;
    public CustomersController(ApplicationDbContext db) => _db = db;

    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(CustomerRegisterDTO dto)
    {
        if (ModelState.IsValid)
        {
            var customer = new Customer
            {
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                RegisterDate = DateTime.Now
            };
            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();
            return RedirectToAction("Create", "Rentals");
        }
        return View(dto);
    }
}