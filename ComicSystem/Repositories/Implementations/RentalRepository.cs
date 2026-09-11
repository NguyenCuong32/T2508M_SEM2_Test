using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;
using ComicSystem.Repositories.Interfaces;

namespace ComicSystem.Repositories.Implementations
{
    public class RentalRepository : GenericRepository<Rental>, IRentalRepository
    {
        public RentalRepository(ComicSystemDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Rental>> GetAllWithCustomerAndDetailsAsync()
        {
            return await _dbSet.AsNoTracking()
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .OrderByDescending(r => r.RentalDate)
                .ToListAsync();
        }

        public async Task<Rental?> GetWithDetailsAsync(int rentalId)
        {
            return await _dbSet.AsNoTracking()
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .FirstOrDefaultAsync(r => r.RentalID == rentalId);
        }

        public async Task<IEnumerable<RentalDetail>> GetRentalDetailsInDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            // Date comparison inclusive of end of day
            var start = startDate.Date;
            var end = endDate.Date.AddDays(1).AddTicks(-1);

            return await _context.RentalDetails.AsNoTracking()
                .Include(rd => rd.Rental)
                    .ThenInclude(r => r!.Customer)
                .Include(rd => rd.ComicBook)
                .Where(rd => rd.Rental != null && 
                             rd.Rental.RentalDate >= start && 
                             rd.Rental.RentalDate <= end)
                .OrderBy(rd => rd.Rental!.RentalDate)
                .ThenBy(rd => rd.ComicBook!.Title)
                .ToListAsync();
        }
    }
}
