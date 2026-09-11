using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;
using ComicSystem.Repositories.Interfaces;

namespace ComicSystem.Repositories.Implementations
{
    public class ComicBookRepository : GenericRepository<ComicBook>, IComicBookRepository
    {
        public ComicBookRepository(ComicSystemDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ComicBook>> SearchAsync(string? keyword)
        {
            var query = _dbSet.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var lowerKeyword = keyword.Trim().ToLower();
                query = query.Where(b => b.Title.ToLower().Contains(lowerKeyword) ||
                                         b.Author.ToLower().Contains(lowerKeyword));
            }
            return await query.OrderBy(b => b.Title).ToListAsync();
        }

        public async Task<bool> HasRentalDetailsAsync(int comicBookId)
        {
            return await _context.RentalDetails.AnyAsync(rd => rd.ComicBookID == comicBookId);
        }
    }
}
