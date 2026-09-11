using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IComicBookRepository
    {
        Task<IEnumerable<ComicBook>> GetAllAsync(string? searchTerm = null);
        Task<ComicBook?> GetByIdAsync(int id);
        Task AddAsync(ComicBook comicBook);
        Task UpdateAsync(ComicBook comicBook);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
