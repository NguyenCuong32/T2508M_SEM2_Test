using Microsoft.EntityFrameworkCore;
using T2508M_SEM_Test.Data;
using T2508M_SEM_Test.Models;
using T2508M_SEM_Test.Repositories.Interfaces;

namespace T2508M_SEM_Test.Repositories.Implementations;

public class ComicBookRepository : IComicBookRepository
{
    private readonly ComicSystemContext _context;

    public ComicBookRepository(ComicSystemContext context)
    {
        _context = context;
    }

    public async Task<List<Comicbook>> GetAllAsync()
    {
        return await _context.Comicbooks
            .OrderByDescending(x => x.ComicBookId)
            .ToListAsync();
    }

    public async Task<Comicbook?> GetByIdAsync(int id)
    {
        return await _context.Comicbooks
            .FirstOrDefaultAsync(x => x.ComicBookId == id);
    }

    public async Task AddAsync(Comicbook comicBook)
    {
        await _context.Comicbooks.AddAsync(comicBook);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Comicbook comicBook)
    {
        _context.Comicbooks.Update(comicBook);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Comicbook comicBook)
    {
        _context.Comicbooks.Remove(comicBook);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Comicbooks
            .AnyAsync(x => x.ComicBookId == id);
    }

    public async Task<bool> HasRentalDetailsAsync(int id)
    {
        return await _context.Rentaldetails
            .AnyAsync(x => x.ComicBookId == id);
    }
}