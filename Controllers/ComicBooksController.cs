using Microsoft.AspNetCore.Mvc;
using T2508M_SEM_Test.DTOs.ComicBooks;
using T2508M_SEM_Test.Services.Interfaces;

namespace T2508M_SEM_Test.Controllers;

public class ComicBooksController : Controller
{
    private readonly IComicBookService _comicBookService;

    public ComicBooksController(IComicBookService comicBookService)
    {
        _comicBookService = comicBookService;
    }

    // GET: /ComicBooks
    public async Task<IActionResult> Index()
    {
        var comicBooks = await _comicBookService.GetAllAsync();

        return View(comicBooks);
    }

    // GET: /ComicBooks/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var comicBook = await _comicBookService.GetByIdAsync(id.Value);

        if (comicBook == null)
        {
            return NotFound();
        }

        return View(comicBook);
    }

    // GET: /ComicBooks/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /ComicBooks/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ComicBookCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        await _comicBookService.CreateAsync(dto);

        TempData["SuccessMessage"] = "Comic book created successfully.";

        return RedirectToAction(nameof(Index));
    }

    // GET: /ComicBooks/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var comicBook = await _comicBookService.GetByIdAsync(id.Value);

        if (comicBook == null)
        {
            return NotFound();
        }

        var dto = new ComicBookUpdateDto
        {
            ComicBookId = comicBook.ComicBookId,
            Title = comicBook.Title,
            Author = comicBook.Author,
            PricePerDay = comicBook.PricePerDay
        };

        return View(dto);
    }

    // POST: /ComicBooks/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ComicBookUpdateDto dto)
    {
        if (id != dto.ComicBookId)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var updated = await _comicBookService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Comic book updated successfully.";

        return RedirectToAction(nameof(Index));
    }

    // GET: /ComicBooks/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var comicBook = await _comicBookService.GetByIdAsync(id.Value);

        if (comicBook == null)
        {
            return NotFound();
        }

        return View(comicBook);
    }

    // POST: /ComicBooks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var deleted = await _comicBookService.DeleteAsync(id);

        if (!deleted)
        {
            TempData["ErrorMessage"] =
                "Comic book does not exist or is already used in a rental.";

            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = "Comic book deleted successfully.";

        return RedirectToAction(nameof(Index));
    }
}