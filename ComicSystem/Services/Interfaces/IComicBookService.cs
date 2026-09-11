using System.Collections.Generic;
using System.Threading.Tasks;
using ComicSystem.Models;

namespace ComicSystem.Services.Interfaces
{
    public interface IComicBookService
    {
        Task<IEnumerable<ComicBook>> GetAllBooksAsync(string? keyword = null);
        Task<ComicBook?> GetBookByIdAsync(int id);
        Task<bool> CreateBookAsync(ComicBook book);
        Task<bool> UpdateBookAsync(ComicBook book);
        Task<(bool Success, string Message)> DeleteBookAsync(int id);
        Task<bool> CanDeleteBookAsync(int id);
    }
}
