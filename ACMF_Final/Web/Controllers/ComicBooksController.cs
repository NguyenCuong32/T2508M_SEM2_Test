using ACMF_Final.Application.Interfaces;
using ACMF_Final.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ACMF_Final.Web.Controllers
{
    public class ComicBooksController : Controller
    {
        private readonly IComicBookService _comicBookService;

        public ComicBooksController(IComicBookService comicBookService)
        {
            _comicBookService = comicBookService;
        }

        // GET: ComicBooks
        public async Task<IActionResult> Index()
        {
            var comicBooks = await _comicBookService.GetAllAsync();
            return View(comicBooks);
        }

        // GET: ComicBooks/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var comicBook = await _comicBookService.GetByIdAsync(id);
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
        public async Task<IActionResult> Create(ComicBook comicBook)
        {
            if (ModelState.IsValid)
            {
                await _comicBookService.AddAsync(comicBook);
                TempData["SuccessMessage"] = "Comic book created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(comicBook);
        }

        // GET: ComicBooks/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var comicBook = await _comicBookService.GetByIdAsync(id);
            if (comicBook == null) return NotFound();
            return View(comicBook);
        }

        // POST: ComicBooks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ComicBook comicBook)
        {
            if (id != comicBook.ComicBookID) return NotFound();

            if (ModelState.IsValid)
            {
                await _comicBookService.UpdateAsync(comicBook);
                TempData["SuccessMessage"] = "Comic book updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(comicBook);
        }

        // GET: ComicBooks/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var comicBook = await _comicBookService.GetByIdAsync(id);
            if (comicBook == null) return NotFound();
            return View(comicBook);
        }

        // POST: ComicBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _comicBookService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Comic book deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
