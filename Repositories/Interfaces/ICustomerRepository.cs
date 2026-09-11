using T2508M_SEM_Test.Models;

namespace T2508M_SEM_Test.Repositories.Interfaces;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync();

    Task<Customer?> GetByIdAsync(int id);

    Task AddAsync(Customer customer);

    Task<bool> ExistsByPhoneAsync(string phoneNumber);
}