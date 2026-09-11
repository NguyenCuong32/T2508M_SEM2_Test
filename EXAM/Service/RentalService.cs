using EXAM.Models;
using EXAM.Models.ViewModels;
using EXAM.Repository;

namespace EXAM.Service
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IComicBookRepository _comicBookRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<RentalService> _logger;

        public RentalService(
            IRentalRepository rentalRepository,
            IComicBookRepository comicBookRepository,
            ICustomerRepository customerRepository,
            ILogger<RentalService> logger)
        {
            _rentalRepository = rentalRepository;
            _comicBookRepository = comicBookRepository;
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Rental>> GetAllRentalsAsync()
        {
            return await _rentalRepository.GetAllRentalsAsync();
        }

        public async Task<Rental?> GetRentalByIdAsync(int id)
        {
            return await _rentalRepository.GetRentalByIdAsync(id);
        }

        public async Task<(bool Success, string? ErrorMessage, int RentalID)> CreateRentalAsync(CreateRentalViewModel model)
        {
            var customer = await _customerRepository.GetByIdAsync(model.CustomerID);
            if (customer == null)
            {
                return (false, "Khách hàng được chọn không tồn tại.", 0);
            }

            var selectedBooks = model.Books.Where(b => b.IsSelected && b.Quantity > 0).ToList();
            if (!selectedBooks.Any())
            {
                return (false, "Vui lòng chọn ít nhất 1 cuốn truyện tranh với số lượng lớn hơn 0.", 0);
            }

            try
            {
                var rental = new Rental
                {
                    CustomerID = model.CustomerID,
                    RentalDate = model.RentalDate,
                    ReturnDate = model.ReturnDate,
                    Status = string.IsNullOrWhiteSpace(model.Status) ? "Đang thuê" : model.Status
                };

                var details = new List<RentalDetail>();
                foreach (var item in selectedBooks)
                {
                    var book = await _comicBookRepository.GetByIdAsync(item.ComicBookID);
                    if (book == null)
                    {
                        return (false, $"Không tìm thấy truyện tranh có mã #{item.ComicBookID}.", 0);
                    }

                    details.Add(new RentalDetail
                    {
                        ComicBookID = item.ComicBookID,
                        Quantity = item.Quantity,
                        PricePerDay = book.PricePerDay
                    });
                }

                int rentalId = await _rentalRepository.CreateRentalWithDetailsAsync(rental, details);
                return (true, null, rentalId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo đơn thuê truyện");
                return (false, "Đã xảy ra lỗi khi tạo đơn thuê truyện: " + ex.Message, 0);
            }
        }

        public async Task<RentalReportViewModel> GetRentalReportAsync(DateTime? startDate, DateTime? endDate)
        {
            var items = await _rentalRepository.GetRentalReportAsync(startDate, endDate);
            return new RentalReportViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                Items = items
            };
        }
    }
}

