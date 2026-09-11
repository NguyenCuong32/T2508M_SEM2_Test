using System.Collections.Generic;
using System.Threading.Tasks;
using ComicSystem.Models;
using ComicSystem.Repositories.Interfaces;
using ComicSystem.Services.Interfaces;

namespace ComicSystem.Services.Implementations
{
    public class ComicBookService : IComicBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ComicBookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ComicBook>> GetAllBooksAsync(string? keyword = null)
        {
            return await _unitOfWork.ComicBooks.SearchAsync(keyword);
        }

        public async Task<ComicBook?> GetBookByIdAsync(int id)
        {
            return await _unitOfWork.ComicBooks.GetByIdAsync(id);
        }

        public async Task<bool> CreateBookAsync(ComicBook book)
        {
            if (book == null || book.PricePerDay <= 0)
                return false;

            await _unitOfWork.ComicBooks.AddAsync(book);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateBookAsync(ComicBook book)
        {
            if (book == null || book.PricePerDay <= 0)
                return false;

            _unitOfWork.ComicBooks.Update(book);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> CanDeleteBookAsync(int id)
        {
            var hasDetails = await _unitOfWork.ComicBooks.HasRentalDetailsAsync(id);
            return !hasDetails;
        }

        public async Task<(bool Success, string Message)> DeleteBookAsync(int id)
        {
            var book = await _unitOfWork.ComicBooks.GetByIdAsync(id);
            if (book == null)
            {
                return (false, "Không tìm thấy truyện tranh cần xóa.");
            }

            var hasDetails = await _unitOfWork.ComicBooks.HasRentalDetailsAsync(id);
            if (hasDetails)
            {
                return (false, "Truyện này đã có lịch sử trong các đơn thuê, không thể xóa để đảm bảo toàn vẹn dữ liệu!");
            }

            _unitOfWork.ComicBooks.Delete(book);
            var saved = await _unitOfWork.SaveChangesAsync() > 0;
            return (saved, saved ? "Xóa truyện thành công." : "Không thể xóa truyện lúc này.");
        }
    }
}
