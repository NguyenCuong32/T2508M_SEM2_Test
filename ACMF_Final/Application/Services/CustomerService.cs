using ACMF_Final.Application.Interfaces;
using ACMF_Final.Domain.Entities;
using ACMF_Final.Domain.Interfaces;

namespace ACMF_Final.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _unitOfWork.Customers.GetAllAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Customers.GetByIdAsync(id);
        }

        public async Task AddAsync(Customer customer)
        {
            customer.RegistrationDate = DateTime.Now;
            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
