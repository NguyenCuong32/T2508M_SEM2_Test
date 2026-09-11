using EXAM.Models;
using EXAM.Repository;
using Microsoft.EntityFrameworkCore;

namespace EXAM.Service
{
    public class ComicBookService : IComicBookService
    {
        private readonly IComicBookRepository _repository;
        private readonly ILogger<ComicBookService> _logger;

        public ComicBookService(IComicBookRepository repository, ILogger<ComicBookService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<ComicBook>> GetAllAsync(string? searchTerm = null)
        {
            return await _repository.GetAllAsync(searchTerm);
        }

        public async Task<ComicBook?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<(bool Success, string? ErrorMessage)> CreateAsync(ComicBook comicBook)
        {
            if (string.IsNullOrWhiteSpace(comicBook.Title))
            {
                return (false, "Tiêu đề truyện không được để trống.");
            }

            if (comicBook.PricePerDay < 0)
            {
                return (false, "Giá thuê mỗi ngày phải là số dương.");
            }

            try
            {
                comicBook.Title = comicBook.Title.Trim();
                comicBook.Author = comicBook.Author?.Trim();

                await _repository.AddAsync(comicBook);
                await _repository.SaveAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi thêm mới truyện tranh");
                return (false, "Đã xảy ra lỗi khi lưu vào cơ sở dữ liệu: " + ex.Message);
            }
        }

        public async Task<(bool Success, string? ErrorMessage)> UpdateAsync(ComicBook comicBook)
        {
            if (string.IsNullOrWhiteSpace(comicBook.Title))
            {
                return (false, "Tiêu đề truyện không được để trống.");
            }

            if (comicBook.PricePerDay < 0)
            {
                return (false, "Giá thuê mỗi ngày phải là số dương.");
            }

            var existing = await _repository.GetByIdAsync(comicBook.ComicBookID);
            if (existing == null)
            {
                return (false, "Không tìm thấy truyện tranh cần cập nhật.");
            }

            try
            {
                existing.Title = comicBook.Title.Trim();
                existing.Author = comicBook.Author?.Trim();
                existing.PricePerDay = comicBook.PricePerDay;

                await _repository.UpdateAsync(existing);
                await _repository.SaveAsync();
                return (true, null);
            }
            catch (DbUpdateConcurrencyException)
            {
                return (false, "Dữ liệu đã bị thay đổi bởi người khác. Vui lòng tải lại trang.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật truyện tranh ID {Id}", comicBook.ComicBookID);
                return (false, "Đã xảy ra lỗi khi cập nhật: " + ex.Message);
            }
        }

        public async Task<(bool Success, string? ErrorMessage)> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
            {
                return (false, "Không tìm thấy truyện tranh cần xóa.");
            }

            // Kiểm tra ràng buộc khóa ngoại với RentalDetails
            var hasRentals = await _repository.HasRentalDetailsAsync(id);
            if (hasRentals)
            {
                return (false, $"Không thể xóa truyện '{existing.Title}' vì đang tồn tại trong lịch sử đơn thuê (RentalDetails)!");
            }

            try
            {
                await _repository.DeleteAsync(id);
                await _repository.SaveAsync();
                return (true, null);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Lỗi ràng buộc CSDL khi xóa truyện ID {Id}", id);
                return (false, "Không thể xóa truyện này do ràng buộc dữ liệu liên quan.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi không xác định khi xóa truyện ID {Id}", id);
                return (false, "Đã xảy ra lỗi khi xóa: " + ex.Message);
            }
        }
    }
}

