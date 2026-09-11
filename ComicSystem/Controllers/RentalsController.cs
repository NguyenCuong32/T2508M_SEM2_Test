using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComicSystem.Services.Interfaces;
using ComicSystem.ViewModels;

namespace ComicSystem.Controllers
{
    public class RentalsController : Controller
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var rentals = await _rentalService.GetAllRentalsAsync();
            return View(rentals);
        }

        // GET: Rentals/Create
        public async Task<IActionResult> Create()
        {
            var model = await _rentalService.PrepareRentalCreateViewModelAsync();
            return View(model);
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentalCreateViewModel model)
        {
            // Filter out empty rows if any
            if (model.Items != null)
            {
                model.Items.RemoveAll(i => i.ComicBookID <= 0 || i.Quantity <= 0);
            }

            if (ModelState.IsValid)
            {
                var (success, message, rentalId) = await _rentalService.CreateRentalAsync(model);
                if (success)
                {
                    TempData["SuccessMessage"] = message;
                    return RedirectToAction(nameof(Details), new { id = rentalId });
                }
                ModelState.AddModelError(string.Empty, message);
            }

            model = await _rentalService.PrepareRentalCreateViewModelAsync(model);
            return View(model);
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var rental = await _rentalService.GetRentalByIdAsync(id.Value);
            if (rental == null) return NotFound();

            return View(rental);
        }

        // POST: Rentals/Return/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id)
        {
            var success = await _rentalService.UpdateRentalStatusAsync(id, "Đã trả");
            if (success)
            {
                TempData["SuccessMessage"] = "Đã cập nhật trạng thái đơn thuê thành \"Đã trả\"!";
            }
            else
            {
                TempData["ErrorMessage"] = "Không thể cập nhật trạng thái đơn thuê.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
