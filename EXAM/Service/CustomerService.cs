using EXAM.Models;
using EXAM.Repository;

namespace EXAM.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository repository, ILogger<CustomerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<(bool Success, string? ErrorMessage)> RegisterCustomerAsync(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.FullName))
            {
                return (false, "Họ tên khách hàng không được để trống.");
            }

            try
            {
                customer.FullName = customer.FullName.Trim();
                customer.PhoneNumber = customer.PhoneNumber?.Trim();
                if (!customer.RegisterDate.HasValue)
                {
                    customer.RegisterDate = DateTime.Now;
                }

                await _repository.AddAsync(customer);
                await _repository.SaveAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi đăng ký khách hàng mới");
                return (false, "Đã xảy ra lỗi khi lưu khách hàng: " + ex.Message);
            }
        }
    }
}

