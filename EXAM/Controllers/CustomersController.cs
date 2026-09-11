using Microsoft.AspNetCore.Mvc;
using EXAM.Models;
using EXAM.Service;

namespace EXAM.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var list = await _customerService.GetAllAsync();
            return View(list);
        }

        // GET: Customers/Create (Câu 2: Đăng ký thông tin khách hàng mới khi lần đầu sử dụng)
        public IActionResult Create()
        {
            return View(new Customer { RegisterDate = DateTime.Now });
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,PhoneNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var (success, error) = await _customerService.RegisterCustomerAsync(customer);
                if (success)
                {
                    TempData["Success"] = $"Đăng ký khách hàng '{customer.FullName}' thành công!";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, error ?? "Có lỗi xảy ra.");
            }

            return View(customer);
        }
    }
}

