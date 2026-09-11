using EXAM.Models;
using Microsoft.EntityFrameworkCore;

namespace EXAM.Repository
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
            IQueryable<ComicBook> query = _context.ComicBooks.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim().ToLower();
                query = query.Where(c => c.Title.ToLower().Contains(term) || (c.Author != null && c.Author.ToLower().Contains(term)));
            }

            return await query.OrderByDescending(c => c.ComicBookID).ToListAsync();
        }

        public async Task<ComicBook?> GetByIdAsync(int id)
        {
            return await _context.ComicBooks
                .Include(c => c.RentalDetails)
                    .ThenInclude(rd => rd.Rental)
                        .ThenInclude(r => r!.Customer)
                .FirstOrDefaultAsync(c => c.ComicBookID == id);
        }

        public async Task AddAsync(ComicBook comicBook)
        {
            await _context.ComicBooks.AddAsync(comicBook);
        }

        public Task UpdateAsync(ComicBook comicBook)
        {
            _context.ComicBooks.Update(comicBook);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var comicBook = await _context.ComicBooks.FindAsync(id);
            if (comicBook != null)
            {
                _context.ComicBooks.Remove(comicBook);
            }
        }

        public async Task<bool> HasRentalDetailsAsync(int comicBookId)
        {
            return await _context.RentalDetails.AnyAsync(r => r.ComicBookID == comicBookId);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

