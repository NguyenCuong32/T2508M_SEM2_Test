using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;
using ComicSystem.Repositories.Interfaces;

namespace ComicSystem.Repositories.Implementations
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ComicSystemDbContext context) : base(context)
        {
        }

        public async Task<Customer?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        }

        public async Task<bool> ExistsPhoneNumberAsync(string phoneNumber, int? excludeCustomerId = null)
        {
            var query = _dbSet.AsQueryable();
            if (excludeCustomerId.HasValue)
            {
                query = query.Where(c => c.CustomerID != excludeCustomerId.Value);
            }
            return await query.AnyAsync(c => c.PhoneNumber == phoneNumber);
        }

        public async Task<Customer?> GetWithRentalsAsync(int customerId)
        {
            return await _dbSet.AsNoTracking()
                .Include(c => c.Rentals)
                    .ThenInclude(r => r.RentalDetails)
                        .ThenInclude(rd => rd.ComicBook)
                .FirstOrDefaultAsync(c => c.CustomerID == customerId);
        }
    }
}
