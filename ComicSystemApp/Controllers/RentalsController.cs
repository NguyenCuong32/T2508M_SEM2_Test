using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ComicSystemApp.Data;
using ComicSystemApp.Models;
using ComicSystemApp.DTOs;
using Microsoft.EntityFrameworkCore;

public class RentalsController : Controller
{
    private readonly ApplicationDbContext _db;
    public RentalsController(ApplicationDbContext db) => _db = db;

    // Câu 3: Trang Thuê sách
    public IActionResult Create()
    {
        ViewData["CustomerID"] = new SelectList(_db.Customers, "CustomerID", "FullName");
        ViewData["ComicBookID"] = new SelectList(_db.ComicBooks, "ComicBookID", "Title");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RentalCreateDTO dto)
    {
        if (ModelState.IsValid)
        {
            var book = await _db.ComicBooks.FindAsync(dto.ComicBookID);
            if (book == null) return NotFound();

            var rental = new Rental
            {
                CustomerID = dto.CustomerID,
                RentalDate = dto.RentalDate,
                ReturnDate = dto.ReturnDate,
                Status = "Đang thuê"
            };
            _db.Rentals.Add(rental);
            await _db.SaveChangesAsync();

            var detail = new RentalDetail
            {
                RentalID = rental.RentalID,
                ComicBookID = dto.ComicBookID,
                Quantity = dto.Quantity,
                PricePerDay = book.PricePerDay
            };
            _db.RentalDetails.Add(detail);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Report));
        }

        ViewData["CustomerID"] = new SelectList(_db.Customers, "CustomerID", "FullName", dto.CustomerID);
        ViewData["ComicBookID"] = new SelectList(_db.ComicBooks, "ComicBookID", "Title", dto.ComicBookID);
        return View(dto);
    }

    // Câu 4: Trang Báo cáo
    public async Task<IActionResult> Report(DateTime? startDate, DateTime? endDate)
    {
        var query = _db.RentalDetails
            .Include(rd => rd.Rental)
            .ThenInclude(r => r!.Customer)
            .Include(rd => rd.ComicBook)
            .AsQueryable();

        if (startDate.HasValue && endDate.HasValue)
        {
            query = query.Where(rd => rd.Rental!.RentalDate >= startDate && rd.Rental.RentalDate <= endDate);
        }

        var list = await query.ToListAsync();

        int no = 1;
        var reportDTOs = list.Select(rd => new RentalReportDTO
        {
            No = no++,
            BookName = rd.ComicBook?.Title ?? "",
            RentalDate = rd.Rental?.RentalDate ?? DateTime.MinValue,
            ReturnDate = rd.Rental?.ReturnDate ?? DateTime.MinValue,
            CustomerName = rd.Rental?.Customer?.FullName ?? "",
            Quantity = rd.Quantity
        }).ToList();

        return View(reportDTOs);
    }
}