using T2508M_SEM_Test.DTOs.ComicBooks;
using T2508M_SEM_Test.Models;
using T2508M_SEM_Test.Repositories.Interfaces;
using T2508M_SEM_Test.Services.Interfaces;

namespace T2508M_SEM_Test.Services.Implementations;

public class ComicBookService : IComicBookService
{
    private readonly IComicBookRepository _comicBookRepository;

    public ComicBookService(IComicBookRepository comicBookRepository)
    {
        _comicBookRepository = comicBookRepository;
    }

    public async Task<List<Comicbook>> GetAllAsync()
    {
        return await _comicBookRepository.GetAllAsync();
    }

    public async Task<Comicbook?> GetByIdAsync(int id)
    {
        return await _comicBookRepository.GetByIdAsync(id);
    }

    public async Task CreateAsync(ComicBookCreateDto dto)
    {
        var comicBook = new Comicbook
        {
            Title = dto.Title.Trim(),
            Author = dto.Author.Trim(),
            PricePerDay = dto.PricePerDay
        };

        await _comicBookRepository.AddAsync(comicBook);
    }

    public async Task<bool> UpdateAsync(int id, ComicBookUpdateDto dto)
    {
        var comicBook = await _comicBookRepository.GetByIdAsync(id);

        if (comicBook == null)
        {
            return false;
        }

        comicBook.Title = dto.Title.Trim();
        comicBook.Author = dto.Author.Trim();
        comicBook.PricePerDay = dto.PricePerDay;

        await _comicBookRepository.UpdateAsync(comicBook);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var comicBook = await _comicBookRepository.GetByIdAsync(id);

        if (comicBook == null)
        {
            return false;
        }

        var hasRentalDetails =
            await _comicBookRepository.HasRentalDetailsAsync(id);

        if (hasRentalDetails)
        {
            return false;
        }

        await _comicBookRepository.DeleteAsync(comicBook);

        return true;
    }
}