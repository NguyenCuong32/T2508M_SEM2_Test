using T2508M_SEM_Test.DTOs.Customers;
using T2508M_SEM_Test.Models;

namespace T2508M_SEM_Test.Services.Interfaces;

public interface ICustomerService
{
    Task<List<Customer>> GetAllAsync();

    Task<Customer?> GetByIdAsync(int id);

    Task<(bool Success, string? ErrorMessage)> CreateAsync(
        CustomerCreateDto dto);
}