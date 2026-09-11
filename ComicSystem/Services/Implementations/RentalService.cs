using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ComicSystem.Models;
using ComicSystem.Repositories.Interfaces;
using ComicSystem.Services.Interfaces;
using ComicSystem.ViewModels;

namespace ComicSystem.Services.Implementations
{
    public class RentalService : IRentalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RentalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Rental>> GetAllRentalsAsync()
        {
            return await _unitOfWork.Rentals.GetAllWithCustomerAndDetailsAsync();
        }

        public async Task<Rental?> GetRentalByIdAsync(int id)
        {
            return await _unitOfWork.Rentals.GetWithDetailsAsync(id);
        }

        public async Task<RentalCreateViewModel> PrepareRentalCreateViewModelAsync(RentalCreateViewModel? model = null)
        {
            model ??= new RentalCreateViewModel();

            var customers = await _unitOfWork.Customers.GetAllAsync();
            model.CustomerList = customers.Select(c => new SelectListItem
            {
                Value = c.CustomerID.ToString(),
                Text = $"{c.FullName} ({c.PhoneNumber})"
            }).ToList();

            var books = await _unitOfWork.ComicBooks.GetAllAsync();
            model.AvailableBooks = books.OrderBy(b => b.Title).ToList();

            return model;
        }

        public async Task<(bool Success, string Message, int RentalId)> CreateRentalAsync(RentalCreateViewModel model)
        {
            if (model == null)
            {
                return (false, "Dữ liệu đơn thuê không hợp lệ.", 0);
            }

            if (model.Items == null || !model.Items.Any())
            {
                return (false, "Vui lòng chọn ít nhất một cuốn truyện tranh.", 0);
            }

            if (model.ReturnDate.Date < model.RentalDate.Date)
            {
                return (false, "Ngày hẹn trả không thể trước ngày thuê.", 0);
            }

            var customer = await _unitOfWork.Customers.GetByIdAsync(model.CustomerID);
            if (customer == null)
            {
                return (false, "Không tìm thấy khách hàng được chọn.", 0);
            }

            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var rental = new Rental
                {
                    CustomerID = model.CustomerID,
                    RentalDate = model.RentalDate,
                    ReturnDate = model.ReturnDate,
                    Status = string.IsNullOrWhiteSpace(model.Status) ? "Đang thuê" : model.Status.Trim()
                };

                await _unitOfWork.Rentals.AddAsync(rental);
                await _unitOfWork.SaveChangesAsync(); // Generates RentalID

                foreach (var item in model.Items)
                {
                    if (item.ComicBookID <= 0 || item.Quantity <= 0)
                        continue;

                    var book = await _unitOfWork.ComicBooks.GetByIdAsync(item.ComicBookID);
                    if (book == null)
                    {
                        await transaction.RollbackAsync();
                        return (false, $"Không tìm thấy truyện tranh có mã {item.ComicBookID}.", 0);
                    }

                    var detail = new RentalDetail
                    {
                        RentalID = rental.RentalID,
                        ComicBookID = book.ComicBookID,
                        Quantity = item.Quantity,
                        PricePerDay = book.PricePerDay // Giá thực tế tại thời điểm thuê
                    };

                    await _unitOfWork.RentalDetails.AddAsync(detail);
                }

                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();

                return (true, "Tạo phiếu thuê truyện tranh thành công!", rental.RentalID);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Lỗi hệ thống khi lưu đơn thuê: {ex.Message}", 0);
            }
        }

        public async Task<bool> UpdateRentalStatusAsync(int rentalId, string status)
        {
            var rental = await _unitOfWork.Rentals.GetByIdAsync(rentalId);
            if (rental == null) return false;

            rental.Status = status;
            _unitOfWork.Rentals.Update(rental);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
