using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Web.Data;
using ComicSystem.Web.Models;

namespace ComicSystem.Web.Controllers;

public class HomeController : Controller
{
    private readonly ComicSystemDbContext _context;

    public HomeController(ComicSystemDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.TotalBooks = await _context.ComicBooks.CountAsync();
        ViewBag.TotalCustomers = await _context.Customers.CountAsync();
        ViewBag.ActiveRentals = await _context.Rentals.CountAsync(r => r.Status == "Đang thuê");
        ViewBag.TotalRentals = await _context.Rentals.CountAsync();

        var recentRentals = await _context.Rentals
            .Include(r => r.Customer)
            .Include(r => r.RentalDetails)
                .ThenInclude(rd => rd.ComicBook)
            .OrderByDescending(r => r.RentalDate)
            .Take(5)
            .ToListAsync();

        return View(recentRentals);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
