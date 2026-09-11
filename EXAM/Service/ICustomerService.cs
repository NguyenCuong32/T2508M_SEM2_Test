using EXAM.Models;

namespace EXAM.Service
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<(bool Success, string? ErrorMessage)> RegisterCustomerAsync(Customer customer);
    }
}

