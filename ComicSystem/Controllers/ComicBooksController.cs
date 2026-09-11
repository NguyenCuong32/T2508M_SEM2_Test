using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComicSystem.Models;
using ComicSystem.Services.Interfaces;

namespace ComicSystem.Controllers
{
    public class ComicBooksController : Controller
    {
        private readonly IComicBookService _comicBookService;

        public ComicBooksController(IComicBookService comicBookService)
        {
            _comicBookService = comicBookService;
        }

        // GET: ComicBooks
        public async Task<IActionResult> Index(string? searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var books = await _comicBookService.GetAllBooksAsync(searchString);
            return View(books);
        }

        // GET: ComicBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var comicBook = await _comicBookService.GetBookByIdAsync(id.Value);
            if (comicBook == null) return NotFound();

            return View(comicBook);
        }

        // GET: ComicBooks/Create
        public IActionResult Create()
        {
            return View(new ComicBook { PricePerDay = 5000 });
        }

        // POST: ComicBooks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComicBookID,Title,Author,PricePerDay")] ComicBook comicBook)
        {
            if (ModelState.IsValid)
            {
                var success = await _comicBookService.CreateBookAsync(comicBook);
                if (success)
                {
                    TempData["SuccessMessage"] = $"Đã thêm mới truyện \"{comicBook.Title}\" thành công!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Không thể thêm truyện tranh vào hệ thống.");
            }
            return View(comicBook);
        }

        // GET: ComicBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var comicBook = await _comicBookService.GetBookByIdAsync(id.Value);
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
                var success = await _comicBookService.UpdateBookAsync(comicBook);
                if (success)
                {
                    TempData["SuccessMessage"] = $"Cập nhật truyện \"{comicBook.Title}\" thành công!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Không thể cập nhật truyện tranh.");
            }
            return View(comicBook);
        }

        // GET: ComicBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var comicBook = await _comicBookService.GetBookByIdAsync(id.Value);
            if (comicBook == null) return NotFound();

            ViewBag.CanDelete = await _comicBookService.CanDeleteBookAsync(id.Value);
            return View(comicBook);
        }

        // POST: ComicBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var (success, message) = await _comicBookService.DeleteBookAsync(id);
            if (success)
            {
                TempData["SuccessMessage"] = message;
            }
            else
            {
                TempData["ErrorMessage"] = message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
