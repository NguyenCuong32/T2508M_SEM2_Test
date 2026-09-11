using System.Threading.Tasks;
using ComicSystem.Models;

namespace ComicSystem.Repositories.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer?> GetByPhoneNumberAsync(string phoneNumber);
        Task<bool> ExistsPhoneNumberAsync(string phoneNumber, int? excludeCustomerId = null);
        Task<Customer?> GetWithRentalsAsync(int customerId);
    }
}
