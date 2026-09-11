using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task<Customer?> GetCustomerByPhoneAsync(string phone)
        {
            return await _customerRepository.GetByPhoneAsync(phone);
        }

        public async Task RegisterCustomerAsync(Customer customer)
        {
            if (customer.RegistrationDate == default)
            {
                customer.RegistrationDate = DateTime.Now;
            }
            await _customerRepository.AddAsync(customer);
        }

        public async Task<bool> CustomerExistsAsync(int id)
        {
            return await _customerRepository.ExistsAsync(id);
        }
    }
}
