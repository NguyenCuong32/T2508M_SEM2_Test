using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Services;
using MyMvcApp.ViewModels;

namespace MyMvcApp.Controllers;

public class RentalsController(ComicService service) : Controller
{
    public async Task<IActionResult> Index() => View(await service.GetRentalsAsync());

    public async Task<IActionResult> Create() => View(await service.GetRentalFormAsync());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RentalCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await service.CreateRentalAsync(model);
                TempData["Success"] = "Đã tạo phiếu thuê.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex) { ModelState.AddModelError("", ex.Message); }
            catch (DbUpdateException) { ModelState.AddModelError("", "Dữ liệu đã thay đổi. Vui lòng kiểm tra khách hàng và truyện rồi thử lại."); }
        }
        await service.PopulateRentalFormAsync(model);
        return View(model);
    }

    public async Task<IActionResult> Report(DateTime? startDate, DateTime? endDate)
    {
        var model = new ReportViewModel
        {
            StartDate = startDate?.Date ?? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
            EndDate = endDate?.Date ?? DateTime.Today
        };
        if (model.StartDate > model.EndDate)
            ModelState.AddModelError("", "Ngày bắt đầu không được sau ngày kết thúc.");
        if (ModelState.IsValid)
        {
            try { model.Rows = await service.GetReportAsync(model.StartDate, model.EndDate); }
            catch (ArgumentException ex) { ModelState.AddModelError("", ex.Message); }
        }
        return View(model);
    }
}
