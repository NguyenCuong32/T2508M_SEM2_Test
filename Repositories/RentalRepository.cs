using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;

namespace ComicSystem.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ComicDbContext _context;

        public RentalRepository(ComicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rental>> GetAllAsync()
        {
            return await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails!)
                    .ThenInclude(rd => rd.ComicBook)
                .OrderByDescending(r => r.RentalID)
                .ToListAsync();
        }

        public async Task AddRentalWithDetailAsync(Rental rental, RentalDetail detail)
        {
            // 1. Add Rental
            await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();

            // 2. Set foreign key and Add RentalDetail
            detail.RentalID = rental.RentalID;
            await _context.RentalDetails.AddAsync(detail);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RentalDetail>> GetReportDetailsAsync(DateTime? startDate, DateTime? endDate)
        {
            var query = _context.RentalDetails
                .Include(rd => rd.Rental)
                    .ThenInclude(r => r!.Customer)
                .Include(rd => rd.ComicBook)
                .AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(rd => rd.Rental!.RentalDate >= startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                var endDay = endDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(rd => rd.Rental!.RentalDate <= endDay);
            }

            return await query
                .OrderBy(rd => rd.Rental!.RentalDate)
                .ThenBy(rd => rd.RentalID)
                .ToListAsync();
        }
    }
}
