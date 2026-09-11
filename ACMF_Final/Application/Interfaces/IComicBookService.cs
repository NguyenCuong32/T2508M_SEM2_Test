using ACMF_Final.Domain.Entities;

namespace ACMF_Final.Application.Interfaces
{
    public interface IComicBookService
    {
        Task<IEnumerable<ComicBook>> GetAllAsync();
        Task<ComicBook?> GetByIdAsync(int id);
        Task AddAsync(ComicBook comicBook);
        Task UpdateAsync(ComicBook comicBook);
        Task DeleteAsync(int id);
    }
}
