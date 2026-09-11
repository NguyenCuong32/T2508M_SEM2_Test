using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using finaltest.Models;

namespace finaltest.Controllers
{
    public class ComicBooksController : Controller
    {
        private readonly ComicDbContext _context;

        public ComicBooksController(ComicDbContext context)
        {
            _context = context;
        }

        // GET: ComicBooks
        public async Task<IActionResult> Index(string searchString)
        {
            var query = _context.ComicBooks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;
            var comicBooks = await query.OrderBy(b => b.Title).ToListAsync();
            return View(comicBooks);
        }

        // GET: ComicBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var comicBook = await _context.ComicBooks
                .Include(c => c.RentalDetails)
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
                TempData["SuccessMessage"] = $"Thêm truyện '{comicBook.Title}' thành công!";
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
                    if (!ComicBookExists(comicBook.ComicBookID)) return NotFound();
                    else throw;
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
                .FirstOrDefaultAsync(m => m.ComicBookID == id);
            if (comicBook == null) return NotFound();

            return View(comicBook);
        }

        // POST: ComicBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comicBook = await _context.ComicBooks.FindAsync(id);
            if (comicBook != null)
            {
                _context.ComicBooks.Remove(comicBook);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Xóa truyện '{comicBook.Title}' thành công!";
            }
            return RedirectToAction(nameof(Index));
        }

        // AJAX API: Get Price for Rental form
        [HttpGet]
        public async Task<IActionResult> GetPrice(int id)
        {
            var comicBook = await _context.ComicBooks.FindAsync(id);
            if (comicBook == null) return NotFound();
            return Json(new { pricePerDay = comicBook.PricePerDay, title = comicBook.Title, author = comicBook.Author });
        }

        private bool ComicBookExists(int id)
        {
            return _context.ComicBooks.Any(e => e.ComicBookID == id);
        }
    }
}
