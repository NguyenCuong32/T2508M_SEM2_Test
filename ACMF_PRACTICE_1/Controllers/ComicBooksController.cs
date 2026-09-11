using ACMF_PRACTICE_1.Data;
using ACMF_PRACTICE_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ACMF_PRACTICE_1.Controllers
{
    public class ComicBooksController : Controller
    {
        private readonly AppDbContext _context;

        public ComicBooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ComicBooks
        public async Task<IActionResult> Index()
        {
            var books = await _context.ComicBooks.ToListAsync();
            return View(books);
        }

        // GET: ComicBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.ComicBooks.FirstOrDefaultAsync(b => b.ComicBookID == id);
            if (book == null) return NotFound();

            return View(book);
        }

        // GET: ComicBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ComicBooks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Author,PricePerDay")] ComicBook book)
        {
            if (ModelState.IsValid)
            {
                _context.ComicBooks.Add(book);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Comic book \"{book.Title}\" created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: ComicBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.ComicBooks.FindAsync(id);
            if (book == null) return NotFound();

            return View(book);
        }

        // POST: ComicBooks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComicBookID,Title,Author,PricePerDay")] ComicBook book)
        {
            if (id != book.ComicBookID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = $"Comic book \"{book.Title}\" updated successfully.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ComicBooks.Any(b => b.ComicBookID == id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: ComicBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.ComicBooks.FirstOrDefaultAsync(b => b.ComicBookID == id);
            if (book == null) return NotFound();

            return View(book);
        }

        // POST: ComicBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.ComicBooks.FindAsync(id);
            if (book != null)
            {
                _context.ComicBooks.Remove(book);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Comic book \"{book.Title}\" deleted.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
