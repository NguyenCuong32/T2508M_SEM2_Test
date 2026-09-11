using Microsoft.EntityFrameworkCore;
using T2508M_SEM_Test.Data;
using T2508M_SEM_Test.Models;
using T2508M_SEM_Test.Repositories.Interfaces;

namespace T2508M_SEM_Test.Repositories.Implementations;

public class CustomerRepository : ICustomerRepository
{
    private readonly ComicSystemContext _context;

    public CustomerRepository(ComicSystemContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _context.Customers
            .OrderByDescending(x => x.CustomerId)
            .ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(x => x.CustomerId == id);
    }

    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByPhoneAsync(string phoneNumber)
    {
        return await _context.Customers
            .AnyAsync(x => x.PhoneNumber == phoneNumber);
    }
}