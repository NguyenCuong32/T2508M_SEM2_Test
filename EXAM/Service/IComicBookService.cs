using EXAM.Models;

namespace EXAM.Service
{
    public interface IComicBookService
    {
        Task<IEnumerable<ComicBook>> GetAllAsync(string? searchTerm = null);
        Task<ComicBook?> GetByIdAsync(int id);
        Task<(bool Success, string? ErrorMessage)> CreateAsync(ComicBook comicBook);
        Task<(bool Success, string? ErrorMessage)> UpdateAsync(ComicBook comicBook);
        Task<(bool Success, string? ErrorMessage)> DeleteAsync(int id);
    }
}

