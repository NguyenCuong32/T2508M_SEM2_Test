using Microsoft.AspNetCore.Mvc;
using EXAM.Models;
using EXAM.Service;

namespace EXAM.Controllers
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
            var comics = await _comicBookService.GetAllAsync(search);
            return View(comics);
        }

        // GET: ComicBooks/Details/5
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

        // GET: ComicBooks/Create
        public IActionResult Create()
        {
            return View(new ComicBook());
        }

        // POST: ComicBooks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComicBookID,Title,Author,PricePerDay")] ComicBook comicBook)
        {
            if (ModelState.IsValid)
            {
                var result = await _comicBookService.CreateAsync(comicBook);
                if (result.Success)
                {
                    TempData["Success"] = $"Thêm mới truyện tranh '{comicBook.Title}' thành công!";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Có lỗi xảy ra khi lưu.");
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

            var comicBook = await _comicBookService.GetByIdAsync(id.Value);
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
                var result = await _comicBookService.UpdateAsync(comicBook);
                if (result.Success)
                {
                    TempData["Success"] = $"Cập nhật truyện tranh '{comicBook.Title}' thành công!";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Có lỗi xảy ra khi cập nhật.");
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

            var comicBook = await _comicBookService.GetByIdAsync(id.Value);
            if (comicBook == null)
            {
                return NotFound();
            }

            return View(comicBook);
        }

        // POST: ComicBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _comicBookService.DeleteAsync(id);
            if (result.Success)
            {
                TempData["Success"] = "Đã xóa truyện tranh thành công!";
            }
            else
            {
                TempData["Error"] = result.ErrorMessage ?? "Không thể xóa truyện tranh.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

