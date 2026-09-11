using System.Collections.Generic;
using System.Threading.Tasks;
using ComicSystem.Models;
using ComicSystem.ViewModels;

namespace ComicSystem.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer?> GetCustomerWithRentalsAsync(int id);
        Task<(bool Success, string Message, Customer? Customer)> RegisterCustomerAsync(CustomerRegisterViewModel model);
        Task<bool> IsPhoneNumberTakenAsync(string phoneNumber, int? excludeId = null);
    }
}
