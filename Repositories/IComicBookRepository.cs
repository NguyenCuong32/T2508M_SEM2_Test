using System.Collections.Generic;
using System.Threading.Tasks;
using ComicSystem.Models;

namespace ComicSystem.Repositories
{
    public interface IComicBookRepository
    {
        Task<IEnumerable<ComicBook>> GetAllAsync();
        Task<ComicBook?> GetByIdAsync(int id);
        Task AddAsync(ComicBook comicBook);
        Task UpdateAsync(ComicBook comicBook);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
