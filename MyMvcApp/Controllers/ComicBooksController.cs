using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models;
using MyMvcApp.Services;

namespace MyMvcApp.Controllers;

public class ComicBooksController(ComicService service) : Controller
{
    public async Task<IActionResult> Index() => View(await service.GetBooksAsync());

    public IActionResult Create() => View("Form", new ComicBook());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Author,PricePerDay")] ComicBook book)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await service.SaveBookAsync(book);
                TempData["Success"] = "Đã thêm truyện.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex) { ModelState.AddModelError("", ex.Message); }
        }
        return View("Form", book);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var book = await service.GetBookAsync(id);
        return book == null ? NotFound() : View("Form", book);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ComicBookId,Title,Author,PricePerDay")] ComicBook book)
    {
        if (id != book.ComicBookId || await service.GetBookAsync(id) == null) return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                await service.SaveBookAsync(book);
                TempData["Success"] = "Đã cập nhật truyện.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex) { ModelState.AddModelError("", ex.Message); }
            catch (KeyNotFoundException) { return NotFound(); }
        }
        return View("Form", book);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await service.DeleteBookAsync(id);
            TempData["Success"] = "Đã xóa truyện.";
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { TempData["Error"] = ex.Message; }
        catch (DbUpdateException) { TempData["Error"] = "Không thể xóa truyện đã có trong phiếu thuê."; }
        return RedirectToAction(nameof(Index));
    }
}
