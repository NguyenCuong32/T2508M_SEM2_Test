using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class ComicBookRepository : IComicBookRepository
    {
        private readonly ComicDbContext _context;

        public ComicBookRepository(ComicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ComicBook>> GetAllAsync(string? searchTerm = null)
        {
            var query = _context.ComicBooks.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(b => b.Title.Contains(searchTerm) || (b.Author != null && b.Author.Contains(searchTerm)));
            }

            return await query.OrderBy(b => b.Title).ToListAsync();
        }

        public async Task<ComicBook?> GetByIdAsync(int id)
        {
            return await _context.ComicBooks.FirstOrDefaultAsync(b => b.ComicBookID == id);
        }

        public async Task AddAsync(ComicBook comicBook)
        {
            await _context.ComicBooks.AddAsync(comicBook);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ComicBook comicBook)
        {
            _context.ComicBooks.Update(comicBook);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comicBook = await _context.ComicBooks.FindAsync(id);
            if (comicBook != null)
            {
                _context.ComicBooks.Remove(comicBook);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.ComicBooks.AnyAsync(b => b.ComicBookID == id);
        }
    }
}
