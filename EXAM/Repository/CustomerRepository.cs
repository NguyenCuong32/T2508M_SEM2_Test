using Microsoft.EntityFrameworkCore;
using EXAM.Models;

namespace EXAM.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ComicDbContext _context;

        public CustomerRepository(ComicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.OrderByDescending(c => c.CustomerID).ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerID == id);
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

