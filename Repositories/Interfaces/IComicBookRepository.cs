using T2508M_SEM_Test.Models;

namespace T2508M_SEM_Test.Repositories.Interfaces;

public interface IComicBookRepository
{
    Task<List<Comicbook>> GetAllAsync();

    Task<Comicbook?> GetByIdAsync(int id);

    Task AddAsync(Comicbook comicBook);

    Task UpdateAsync(Comicbook comicBook);

    Task DeleteAsync(Comicbook comicBook);

    Task<bool> ExistsAsync(int id);

    Task<bool> HasRentalDetailsAsync(int id);
}