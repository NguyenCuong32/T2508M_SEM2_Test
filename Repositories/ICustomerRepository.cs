using System.Collections.Generic;
using System.Threading.Tasks;
using ComicSystem.Models;

namespace ComicSystem.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task AddAsync(Customer customer);
    }
}
