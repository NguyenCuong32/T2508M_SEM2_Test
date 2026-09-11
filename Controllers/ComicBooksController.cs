using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;

namespace ComicSystem.Controllers
{
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

            var comicBooks = from b in _context.ComicBooks
                             select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                comicBooks = comicBooks.Where(s => s.Title.Contains(searchString) 
                                               || s.Author.Contains(searchString));
            }

            return View(await comicBooks.OrderBy(b => b.Title).ToListAsync());
        }

        // GET: ComicBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _context.ComicBooks
                .Include(c => c.RentalDetails)
                    .ThenInclude(rd => rd.Rental)
                        .ThenInclude(r => r!.Customer)
                .FirstOrDefaultAsync(m => m.ComicBookID == id);

            if (comicBook == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> Create([Bind("Title,Author,PricePerDay")] ComicBook comicBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comicBook);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Thêm truyện \"{comicBook.Title}\" thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(comicBook);
        }

        // GET: ComicBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _context.ComicBooks.FindAsync(id);
            if (comicBook == null)
            {
                return NotFound();
            }
            return View(comicBook);
        }

        // POST: ComicBooks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComicBookID,Title,Author,PricePerDay")] ComicBook comicBook)
        {
            if (id != comicBook.ComicBookID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comicBook);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Cập nhật truyện \"{comicBook.Title}\" thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComicBookExists(comicBook.ComicBookID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(comicBook);
        }

        // GET: ComicBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _context.ComicBooks
                .Include(c => c.RentalDetails)
                .FirstOrDefaultAsync(m => m.ComicBookID == id);

            if (comicBook == null)
            {
                return NotFound();
            }

            ViewBag.HasActiveRentals = comicBook.RentalDetails.Any();

            return View(comicBook);
        }

        // POST: ComicBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comicBook = await _context.ComicBooks
                .Include(c => c.RentalDetails)
                .FirstOrDefaultAsync(c => c.ComicBookID == id);

            if (comicBook != null)
            {
                if (comicBook.RentalDetails.Any())
                {
                    TempData["ErrorMessage"] = $"Không thể xóa truyện \"{comicBook.Title}\" vì đã có lịch sử phiếu thuê!";
                    return RedirectToAction(nameof(Index));
                }

                _context.ComicBooks.Remove(comicBook);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Xóa truyện \"{comicBook.Title}\" thành công!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ComicBookExists(int id)
        {
            return _context.ComicBooks.Any(e => e.ComicBookID == id);
        }
    }
}
