using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Repository
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ComicDbContext _context;

        public RentalRepository(ComicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rental>> GetAllWithDetailsAsync()
        {
            return await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .OrderByDescending(r => r.RentalDate)
                .ThenByDescending(r => r.RentalID)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Rental?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .FirstOrDefaultAsync(r => r.RentalID == id);
        }

        public async Task<Rental> CreateRentalWithDetailAsync(Rental rental, RentalDetail detail)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Rentals.AddAsync(rental);
                await _context.SaveChangesAsync();

                detail.RentalID = rental.RentalID;
                await _context.RentalDetails.AddAsync(detail);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return rental;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<RentalReportItem>> GetReportAsync(DateTime? startDate, DateTime? endDate)
        {
            var query = _context.RentalDetails
                .Include(rd => rd.Rental)
                    .ThenInclude(r => r!.Customer)
                .Include(rd => rd.ComicBook)
                .AsNoTracking()
                .AsQueryable();

            if (startDate.HasValue)
            {
                var start = startDate.Value.Date;
                query = query.Where(rd => rd.Rental!.RentalDate >= start);
            }

            if (endDate.HasValue)
            {
                var end = endDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(rd => rd.Rental!.RentalDate <= end);
            }

            var rawItems = await query
                .OrderBy(rd => rd.Rental!.RentalDate)
                .ThenBy(rd => rd.RentalDetailID)
                .Select(rd => new
                {
                    BookName = rd.ComicBook != null ? rd.ComicBook.Title : "Unknown",
                    RentalDate = rd.Rental != null ? rd.Rental.RentalDate : DateTime.MinValue,
                    ReturnDate = rd.Rental != null ? rd.Rental.ReturnDate : DateTime.MinValue,
                    CustomerName = rd.Rental != null && rd.Rental.Customer != null ? rd.Rental.Customer.FullName : "Unknown",
                    Quantity = rd.Quantity,
                    PricePerDay = rd.PricePerDay
                })
                .ToListAsync();

            var result = new List<RentalReportItem>();
            int index = 1;
            foreach (var item in rawItems)
            {
                result.Add(new RentalReportItem
                {
                    No = index++,
                    Bookname = item.BookName,
                    Rentaldate = item.RentalDate,
                    Returndate = item.ReturnDate,
                    Customername = item.CustomerName,
                    Quantity = item.Quantity,
                    PricePerDay = item.PricePerDay
                });
            }

            return result;
        }
    }
}
