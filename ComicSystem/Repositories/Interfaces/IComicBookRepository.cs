using System.Collections.Generic;
using System.Threading.Tasks;
using ComicSystem.Models;

namespace ComicSystem.Repositories.Interfaces
{
    public interface IComicBookRepository : IGenericRepository<ComicBook>
    {
        Task<IEnumerable<ComicBook>> SearchAsync(string? keyword);
        Task<bool> HasRentalDetailsAsync(int comicBookId);
    }
}
