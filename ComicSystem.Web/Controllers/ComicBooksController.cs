using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Web.Data;
using ComicSystem.Web.Models;

namespace ComicSystem.Web.Controllers;

public class ComicBooksController : Controller
{
    private readonly ComicSystemDbContext _context;

    public ComicBooksController(ComicSystemDbContext context)
    {
        _context = context;
    }

    // GET: ComicBooks
    public async Task<IActionResult> Index(string? searchString)
    {
        ViewData["CurrentFilter"] = searchString;

        var booksQuery = _context.ComicBooks
            .Include(b => b.RentalDetails)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            var search = searchString.Trim();
            booksQuery = booksQuery.Where(b => 
                b.Title.Contains(search) || 
                b.Author.Contains(search));
        }

        var books = await booksQuery.OrderBy(b => b.Title).ToListAsync();
        return View(books);
    }

    // GET: ComicBooks/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var comicBook = await _context.ComicBooks
            .Include(b => b.RentalDetails)
                .ThenInclude(rd => rd.Rental)
                    .ThenInclude(r => r!.Customer)
            .FirstOrDefaultAsync(m => m.ComicBookID == id);

        if (comicBook == null) return NotFound();

        return View(comicBook);
    }

    // GET: ComicBooks/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ComicBooks/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ComicBookID,Title,Author,PricePerDay")] ComicBook comicBook)
    {
        if (ModelState.IsValid)
        {
            _context.Add(comicBook);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Thêm mới truyện '{comicBook.Title}' thành công!";
            return RedirectToAction(nameof(Index));
        }
        return View(comicBook);
    }

    // GET: ComicBooks/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var comicBook = await _context.ComicBooks.FindAsync(id);
        if (comicBook == null) return NotFound();

        return View(comicBook);
    }

    // POST: ComicBooks/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ComicBookID,Title,Author,PricePerDay")] ComicBook comicBook)
    {
        if (id != comicBook.ComicBookID) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(comicBook);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Cập nhật truyện '{comicBook.Title}' thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ComicBookExists(comicBook.ComicBookID))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(comicBook);
    }

    // GET: ComicBooks/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var comicBook = await _context.ComicBooks
            .Include(b => b.RentalDetails)
            .FirstOrDefaultAsync(m => m.ComicBookID == id);

        if (comicBook == null) return NotFound();

        // Check if book has been rented
        ViewBag.HasRentals = comicBook.RentalDetails.Any();

        return View(comicBook);
    }

    // POST: ComicBooks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var comicBook = await _context.ComicBooks
            .Include(b => b.RentalDetails)
            .FirstOrDefaultAsync(b => b.ComicBookID == id);

        if (comicBook != null)
        {
            if (comicBook.RentalDetails.Any())
            {
                TempData["ErrorMessage"] = $"Không thể xóa truyện '{comicBook.Title}' vì đã có lịch sử thuê trong hệ thống!";
                return RedirectToAction(nameof(Index));
            }

            _context.ComicBooks.Remove(comicBook);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Đã xóa truyện '{comicBook.Title}' thành công!";
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> ComicBookExists(int id)
    {
        return await _context.ComicBooks.AnyAsync(e => e.ComicBookID == id);
    }
}
