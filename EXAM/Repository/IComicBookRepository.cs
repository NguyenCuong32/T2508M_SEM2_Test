using EXAM.Models;

namespace EXAM.Repository
{
    public interface IComicBookRepository
    {
        Task<IEnumerable<ComicBook>> GetAllAsync(string? searchTerm = null);
        Task<ComicBook?> GetByIdAsync(int id);
        Task AddAsync(ComicBook comicBook);
        Task UpdateAsync(ComicBook comicBook);
        Task DeleteAsync(int id);
        Task<bool> HasRentalDetailsAsync(int comicBookId);
        Task SaveAsync();
    }
}

