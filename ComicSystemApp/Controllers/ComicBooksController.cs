using Microsoft.AspNetCore.Mvc;
using ComicSystemApp.Data;
using ComicSystemApp.Models;
using ComicSystemApp.DTOs;
using Microsoft.EntityFrameworkCore;

public class ComicBooksController : Controller
{
    private readonly ApplicationDbContext _db;
    public ComicBooksController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var books = await _db.ComicBooks
            .Select(b => new ComicBookDTO
            {
                ComicBookID = b.ComicBookID,
                Title = b.Title,
                Author = b.Author,
                PricePerDay = b.PricePerDay
            }).ToListAsync();

        return View(books);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(ComicBookDTO dto)
    {
        if (ModelState.IsValid)
        {
            var entity = new ComicBook
            {
                Title = dto.Title,
                Author = dto.Author,
                PricePerDay = dto.PricePerDay
            };
            _db.ComicBooks.Add(entity);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(dto);
    }
}