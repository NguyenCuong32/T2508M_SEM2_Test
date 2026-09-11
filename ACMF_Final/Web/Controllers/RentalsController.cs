using ACMF_Final.Application.DTOs;
using ACMF_Final.Application.Interfaces;
using ACMF_Final.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ACMF_Final.Web.Controllers
{
    public class RentalsController : Controller
    {
        private readonly IRentalService _rentalService;
        private readonly ICustomerService _customerService;
        private readonly IComicBookService _comicBookService;

        public RentalsController(IRentalService rentalService, ICustomerService customerService, IComicBookService comicBookService)
        {
            _rentalService = rentalService;
            _customerService = customerService;
            _comicBookService = comicBookService;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var rentals = await _rentalService.GetAllAsync();
            return View(rentals);
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var rental = await _rentalService.GetByIdAsync(id);
            if (rental == null) return NotFound();
            return View(rental);
        }

        // GET: Rentals/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = await BuildCreateRentalViewModel();
            return View(viewModel);
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRentalViewModel viewModel)
        {
            // Filter only selected items with quantity > 0
            var selectedItems = viewModel.Items
                .Where(i => i.IsSelected && i.Quantity > 0)
                .ToList();

            if (selectedItems.Count == 0)
            {
                ModelState.AddModelError("", "Please select at least one comic book.");
                var rebuiltViewModel = await BuildCreateRentalViewModel();
                rebuiltViewModel.CustomerID = viewModel.CustomerID;
                rebuiltViewModel.RentalDate = viewModel.RentalDate;
                rebuiltViewModel.ReturnDate = viewModel.ReturnDate;
                return View(rebuiltViewModel);
            }

            if (viewModel.ReturnDate <= viewModel.RentalDate)
            {
                ModelState.AddModelError("ReturnDate", "Return date must be after rental date.");
                var rebuiltViewModel = await BuildCreateRentalViewModel();
                rebuiltViewModel.CustomerID = viewModel.CustomerID;
                rebuiltViewModel.RentalDate = viewModel.RentalDate;
                rebuiltViewModel.ReturnDate = viewModel.ReturnDate;
                return View(rebuiltViewModel);
            }

            var dto = new CreateRentalDTO
            {
                CustomerID = viewModel.CustomerID,
                RentalDate = viewModel.RentalDate,
                ReturnDate = viewModel.ReturnDate,
                Details = selectedItems.Select(i => new RentalDetailDTO
                {
                    ComicBookID = i.ComicBookID,
                    Quantity = i.Quantity
                }).ToList()
            };

            await _rentalService.CreateRentalAsync(dto);
            TempData["SuccessMessage"] = "Rental created successfully!";
            return RedirectToAction(nameof(Index));
        }

        private async Task<CreateRentalViewModel> BuildCreateRentalViewModel()
        {
            var customers = await _customerService.GetAllAsync();
            var comicBooks = (await _comicBookService.GetAllAsync()).ToList();

            return new CreateRentalViewModel
            {
                CustomerList = new SelectList(customers, "CustomerID", "FullName"),
                AvailableComics = comicBooks,
                Items = comicBooks.Select(c => new RentalItemViewModel
                {
                    ComicBookID = c.ComicBookID,
                    Title = c.Title,
                    Author = c.Author,
                    PricePerDay = c.PricePerDay,
                    IsSelected = false,
                    Quantity = 1
                }).ToList()
            };
        }
    }
}
