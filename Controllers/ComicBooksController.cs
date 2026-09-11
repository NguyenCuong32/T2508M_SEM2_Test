using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComicSystem.Models;
using ComicSystem.Repositories;

namespace ComicSystem.Controllers
{
    public class ComicBooksController : Controller
    {
        private readonly IComicBookRepository _comicBookRepository;

        public ComicBooksController(IComicBookRepository comicBookRepository)
        {
            _comicBookRepository = comicBookRepository;
        }

        // GET: ComicBooks
        public async Task<IActionResult> Index()
        {
            var books = await _comicBookRepository.GetAllAsync();
            return View(books);
        }

        // GET: ComicBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _comicBookRepository.GetByIdAsync(id.Value);
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
        public async Task<IActionResult> Create([Bind("ComicBookID,Title,Author,PricePerDay")] ComicBook comicBook)
        {
            if (ModelState.IsValid)
            {
                await _comicBookRepository.AddAsync(comicBook);
                TempData["SuccessMessage"] = "Thêm mới truyện thành công!";
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

            var comicBook = await _comicBookRepository.GetByIdAsync(id.Value);
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
                if (!await _comicBookRepository.ExistsAsync(comicBook.ComicBookID))
                {
                    return NotFound();
                }

                await _comicBookRepository.UpdateAsync(comicBook);
                TempData["SuccessMessage"] = "Cập nhật truyện thành công!";
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

            var comicBook = await _comicBookRepository.GetByIdAsync(id.Value);
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
            await _comicBookRepository.DeleteAsync(id);
            TempData["SuccessMessage"] = "Xóa truyện thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
