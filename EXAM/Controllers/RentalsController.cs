using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EXAM.Models.ViewModels;
using EXAM.Service;

namespace EXAM.Controllers
{
    public class RentalsController : Controller
    {
        private readonly IRentalService _rentalService;
        private readonly ICustomerService _customerService;
        private readonly IComicBookService _comicBookService;

        public RentalsController(
            IRentalService rentalService,
            ICustomerService customerService,
            IComicBookService comicBookService)
        {
            _rentalService = rentalService;
            _customerService = customerService;
            _comicBookService = comicBookService;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var rentals = await _rentalService.GetAllRentalsAsync();
            return View(rentals);
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var rental = await _rentalService.GetRentalByIdAsync(id.Value);
            if (rental == null) return NotFound();

            return View(rental);
        }

        // GET: Rentals/Create (Câu 3: Thêm đơn thuê sách mới)
        public async Task<IActionResult> Create()
        {
            await PopulateCustomersDropDownList();

            var comics = await _comicBookService.GetAllAsync();
            var model = new CreateRentalViewModel
            {
                RentalDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddDays(7),
                Status = "Đang thuê",
                Books = comics.Select(c => new RentalBookItemInput
                {
                    ComicBookID = c.ComicBookID,
                    Title = c.Title,
                    PricePerDay = c.PricePerDay,
                    IsSelected = false,
                    Quantity = 1
                }).ToList()
            };

            return View(model);
        }

        // POST: Rentals/Create (Câu 3)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRentalViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (success, error, rentalId) = await _rentalService.CreateRentalAsync(model);
                if (success)
                {
                    TempData["Success"] = $"Tạo đơn thuê sách mã #{rentalId} thành công (đã chèn đồng thời vào Rentals & RentalDetails)!";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, error ?? "Có lỗi xảy ra khi tạo đơn thuê.");
            }

            await PopulateCustomersDropDownList(model.CustomerID);
            return View(model);
        }

        // GET: Rentals/Report (Câu 4: Báo cáo danh sách thuê sách theo khoảng thời gian)
        public async Task<IActionResult> Report(DateTime? startDate, DateTime? endDate)
        {
            // Nếu người dùng chưa chọn ngày, đặt mặc định từ đầu tháng trước đến hết tháng hiện tại để xem dữ liệu
            if (!startDate.HasValue && !endDate.HasValue)
            {
                startDate = new DateTime(2024, 10, 1);
                endDate = DateTime.Today.AddDays(30);
            }

            var report = await _rentalService.GetRentalReportAsync(startDate, endDate);
            return View(report);
        }

        private async Task PopulateCustomersDropDownList(object? selectedCustomer = null)
        {
            var customers = await _customerService.GetAllAsync();
            ViewBag.CustomerID = new SelectList(customers, "CustomerID", "FullName", selectedCustomer);
        }
    }
}

