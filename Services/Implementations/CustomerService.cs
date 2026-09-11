using T2508M_SEM_Test.DTOs.Customers;
using T2508M_SEM_Test.Models;
using T2508M_SEM_Test.Repositories.Interfaces;
using T2508M_SEM_Test.Services.Interfaces;

namespace T2508M_SEM_Test.Services.Implementations;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _customerRepository.GetAllAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _customerRepository.GetByIdAsync(id);
    }

    public async Task<(bool Success, string? ErrorMessage)> CreateAsync(
        CustomerCreateDto dto)
    {
        var phoneNumber = dto.PhoneNumber.Trim();

        if (await _customerRepository.ExistsByPhoneAsync(phoneNumber))
        {
            return (
                false,
                "A customer with this phone number already exists."
            );
        }

        var customer = new Customer
        {
            FullName = dto.FullName.Trim(),
            PhoneNumber = phoneNumber,
            RegistrationDate = dto.RegistrationDate
        };

        await _customerRepository.AddAsync(customer);

        return (true, null);
    }
}