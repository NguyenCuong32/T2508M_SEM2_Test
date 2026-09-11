using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using T2508M_SEM_Test.DTOs.Rentals;
using T2508M_SEM_Test.Services.Interfaces;

namespace T2508M_SEM_Test.Controllers;

public class RentalsController : Controller
{
    private readonly IRentalService _rentalService;
    private readonly ICustomerService _customerService;
    private readonly IComicBookService _comicBookService;

    public RentalsController(
        IRentalService rentalService,
        ICustomerService customerService,
        IComicBookService comicBookService)
    {
        _rentalService = rentalService;
        _customerService = customerService;
        _comicBookService = comicBookService;
    }

    // GET: /Rentals
    public async Task<IActionResult> Index()
    {
        var rentals = await _rentalService.GetAllAsync();

        return View(rentals);
    }

    // GET: /Rentals/Create
    public async Task<IActionResult> Create()
    {
        await LoadDropdowns();

        var dto = new RentalCreateDto
        {
            RentalDate = DateTime.Now,
            ReturnDate = DateTime.Now.AddDays(1),
            Quantity = 1
        };

        return View(dto);
    }

    // POST: /Rentals/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RentalCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdowns();
            return View(dto);
        }

        var result = await _rentalService.CreateAsync(dto);

        if (!result.Success)
        {
            ModelState.AddModelError(
                string.Empty,
                result.ErrorMessage!);

            await LoadDropdowns();

            return View(dto);
        }

        TempData["SuccessMessage"] =
            "Book rental created successfully.";

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadDropdowns()
    {
        var customers = await _customerService.GetAllAsync();

        var comicBooks = await _comicBookService.GetAllAsync();

        ViewBag.Customers = new SelectList(
            customers,
            "CustomerId",
            "FullName");

        ViewBag.ComicBooks = new SelectList(
            comicBooks,
            "ComicBookId",
            "Title");
    }
}