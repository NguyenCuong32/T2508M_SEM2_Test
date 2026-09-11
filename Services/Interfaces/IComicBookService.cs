using T2508M_SEM_Test.DTOs.ComicBooks;
using T2508M_SEM_Test.Models;

namespace T2508M_SEM_Test.Services.Interfaces;

public interface IComicBookService
{
    Task<List<Comicbook>> GetAllAsync();

    Task<Comicbook?> GetByIdAsync(int id);

    Task CreateAsync(ComicBookCreateDto dto);

    Task<bool> UpdateAsync(int id, ComicBookUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}