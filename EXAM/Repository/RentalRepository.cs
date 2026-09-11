using Microsoft.EntityFrameworkCore;
using EXAM.Models;
using EXAM.Models.ViewModels;

namespace EXAM.Repository
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ComicDbContext _context;

        public RentalRepository(ComicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rental>> GetAllRentalsAsync()
        {
            return await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .OrderByDescending(r => r.RentalDate)
                .ToListAsync();
        }

        public async Task<Rental?> GetRentalByIdAsync(int id)
        {
            return await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .FirstOrDefaultAsync(r => r.RentalID == id);
        }

        public async Task<int> CreateRentalWithDetailsAsync(Rental rental, IEnumerable<RentalDetail> details)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Rentals.AddAsync(rental);
                await _context.SaveChangesAsync();

                foreach (var detail in details)
                {
                    detail.RentalID = rental.RentalID;
                    await _context.RentalDetails.AddAsync(detail);
                }
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return rental.RentalID;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<RentalReportItemViewModel>> GetRentalReportAsync(DateTime? startDate, DateTime? endDate)
        {
            var query = from r in _context.Rentals
                        join c in _context.Customers on r.CustomerID equals c.CustomerID
                        join rd in _context.RentalDetails on r.RentalID equals rd.RentalID
                        join b in _context.ComicBooks on rd.ComicBookID equals b.ComicBookID
                        select new { r, c, rd, b };

            if (startDate.HasValue)
            {
                var start = startDate.Value.Date;
                query = query.Where(x => x.r.RentalDate >= start);
            }

            if (endDate.HasValue)
            {
                var end = endDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(x => x.r.RentalDate <= end);
            }

            var rawList = await query.OrderBy(x => x.r.RentalDate).ToListAsync();

            int index = 1;
            return rawList.Select(x => new RentalReportItemViewModel
            {
                No = index++,
                BookName = x.b.Title,
                RentalDate = x.r.RentalDate,
                ReturnDate = x.r.ReturnDate,
                CustomerName = x.c.FullName,
                Quantity = x.rd.Quantity
            }).ToList();
        }
    }
}

