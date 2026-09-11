using Microsoft.EntityFrameworkCore;
using T2508M_SEM_Test.Data;
using T2508M_SEM_Test.Models;
using T2508M_SEM_Test.Repositories.Interfaces;

namespace T2508M_SEM_Test.Repositories.Implementations;

public class RentalRepository : IRentalRepository
{
    private readonly ComicSystemContext _context;

    public RentalRepository(ComicSystemContext context)
    {
        _context = context;
    }

    public async Task<List<Rental>> GetAllAsync()
    {
        return await _context.Rentals
            .Include(x => x.Customer)
            .Include(x => x.Rentaldetails)
                .ThenInclude(x => x.ComicBook)
            .OrderByDescending(x => x.RentalId)
            .ToListAsync();
    }

    public async Task<Rental?> GetByIdAsync(int id)
    {
        return await _context.Rentals
            .Include(x => x.Customer)
            .Include(x => x.Rentaldetails)
                .ThenInclude(x => x.ComicBook)
            .FirstOrDefaultAsync(x => x.RentalId == id);
    }

    public async Task CreateRentalAsync(
        Rental rental,
        Rentaldetail rentalDetail)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            // Insert Rentals
            await _context.Rentals.AddAsync(rental);

            await _context.SaveChangesAsync();

            // RentalId has now been generated
            rentalDetail.RentalId = rental.RentalId;

            // Insert RentalDetails
            await _context.Rentaldetails.AddAsync(rentalDetail);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}