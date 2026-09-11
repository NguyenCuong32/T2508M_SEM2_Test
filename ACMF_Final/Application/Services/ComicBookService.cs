using ACMF_Final.Application.Interfaces;
using ACMF_Final.Domain.Entities;
using ACMF_Final.Domain.Interfaces;

namespace ACMF_Final.Application.Services
{
    public class ComicBookService : IComicBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ComicBookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ComicBook>> GetAllAsync()
        {
            return await _unitOfWork.ComicBooks.GetAllAsync();
        }

        public async Task<ComicBook?> GetByIdAsync(int id)
        {
            return await _unitOfWork.ComicBooks.GetByIdAsync(id);
        }

        public async Task AddAsync(ComicBook comicBook)
        {
            await _unitOfWork.ComicBooks.AddAsync(comicBook);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(ComicBook comicBook)
        {
            _unitOfWork.ComicBooks.Update(comicBook);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comicBook = await _unitOfWork.ComicBooks.GetByIdAsync(id);
            if (comicBook != null)
            {
                _unitOfWork.ComicBooks.Remove(comicBook);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
