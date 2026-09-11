using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IComicBookService
    {
        Task<IEnumerable<ComicBook>> GetAllComicBooksAsync(string? searchTerm = null);
        Task<ComicBook?> GetComicBookByIdAsync(int id);
        Task CreateComicBookAsync(ComicBook comicBook);
        Task UpdateComicBookAsync(ComicBook comicBook);
        Task DeleteComicBookAsync(int id);
        Task<bool> ComicBookExistsAsync(int id);
    }
}
