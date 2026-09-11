using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicSystem.Models;
using ComicSystem.Repositories.Interfaces;
using ComicSystem.Services.Interfaces;
using ComicSystem.ViewModels;

namespace ComicSystem.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _unitOfWork.Customers.GetAllAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _unitOfWork.Customers.GetByIdAsync(id);
        }

        public async Task<Customer?> GetCustomerWithRentalsAsync(int id)
        {
            return await _unitOfWork.Customers.GetWithRentalsAsync(id);
        }

        public async Task<bool> IsPhoneNumberTakenAsync(string phoneNumber, int? excludeId = null)
        {
            return await _unitOfWork.Customers.ExistsPhoneNumberAsync(phoneNumber, excludeId);
        }

        public async Task<(bool Success, string Message, Customer? Customer)> RegisterCustomerAsync(CustomerRegisterViewModel model)
        {
            if (model == null)
            {
                return (false, "Dữ liệu đăng ký không hợp lệ.", null);
            }

            var phoneClean = model.PhoneNumber.Trim();
            if (await _unitOfWork.Customers.ExistsPhoneNumberAsync(phoneClean))
            {
                return (false, $"Số điện thoại {phoneClean} đã được đăng ký trong hệ thống.", null);
            }

            var customer = new Customer
            {
                FullName = model.FullName.Trim(),
                PhoneNumber = phoneClean,
                RegistrationDate = model.RegistrationDate == default ? DateTime.Now : model.RegistrationDate
            };

            await _unitOfWork.Customers.AddAsync(customer);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                return (true, "Đăng ký thông tin khách hàng thành công!", customer);
            }

            return (false, "Có lỗi xảy ra trong quá trình lưu dữ liệu.", null);
        }
    }
}
