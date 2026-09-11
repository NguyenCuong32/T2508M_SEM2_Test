using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;

namespace ComicSystem.Repositories
{
    public class ComicBookRepository : IComicBookRepository
    {
        private readonly ComicDbContext _context;

        public ComicBookRepository(ComicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ComicBook>> GetAllAsync()
        {
            return await _context.ComicBooks.ToListAsync();
        }

        public async Task<ComicBook?> GetByIdAsync(int id)
        {
            return await _context.ComicBooks.FirstOrDefaultAsync(m => m.ComicBookID == id);
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
                // Remove related RentalDetails first to satisfy Foreign Key constraints
                var relatedDetails = await _context.RentalDetails
                    .Where(rd => rd.ComicBookID == id)
                    .ToListAsync();

                if (relatedDetails.Count > 0)
                {
                    _context.RentalDetails.RemoveRange(relatedDetails);
                }

                _context.ComicBooks.Remove(comicBook);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.ComicBooks.AnyAsync(e => e.ComicBookID == id);
        }
    }
}
