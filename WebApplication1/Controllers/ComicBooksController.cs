using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class ComicBooksController : Controller
    {
        private readonly IComicBookService _comicBookService;

        public ComicBooksController(IComicBookService comicBookService)
        {
            _comicBookService = comicBookService;
        }

        // GET: ComicBooks
        public async Task<IActionResult> Index(string? search)
        {
            ViewBag.CurrentSearch = search;
            var books = await _comicBookService.GetAllComicBooksAsync(search);
            return View(books);
        }

        // GET: ComicBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _comicBookService.GetComicBookByIdAsync(id.Value);
            if (book == null) return NotFound();

            return View(book);
        }

        // GET: ComicBooks/Create
        public IActionResult Create()
        {
            return View(new ComicBook { PricePerDay = 5000 });
        }

        // POST: ComicBooks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Author,PricePerDay")] ComicBook comicBook)
        {
            if (ModelState.IsValid)
            {
                await _comicBookService.CreateComicBookAsync(comicBook);
                TempData["SuccessMessage"] = $"Thêm truyện '{comicBook.Title}' thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(comicBook);
        }

        // GET: ComicBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var book = await _comicBookService.GetComicBookByIdAsync(id.Value);
            if (book == null) return NotFound();

            return View(book);
        }

        // POST: ComicBooks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComicBookID,Title,Author,PricePerDay")] ComicBook comicBook)
        {
            if (id != comicBook.ComicBookID) return NotFound();

            if (ModelState.IsValid)
            {
                await _comicBookService.UpdateComicBookAsync(comicBook);
                TempData["SuccessMessage"] = $"Cập nhật truyện '{comicBook.Title}' thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(comicBook);
        }

        // GET: ComicBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var book = await _comicBookService.GetComicBookByIdAsync(id.Value);
            if (book == null) return NotFound();

            return View(book);
        }

        // POST: ComicBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _comicBookService.GetComicBookByIdAsync(id);
            await _comicBookService.DeleteComicBookAsync(id);
            TempData["SuccessMessage"] = $"Đã xóa truyện '{book?.Title ?? id.ToString()}'!";
            return RedirectToAction(nameof(Index));
        }
    }
}
