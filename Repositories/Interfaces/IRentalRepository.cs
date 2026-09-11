using T2508M_SEM_Test.Models;

namespace T2508M_SEM_Test.Repositories.Interfaces;

public interface IRentalRepository
{
    Task<List<Rental>> GetAllAsync();

    Task<Rental?> GetByIdAsync(int id);

    Task CreateRentalAsync(Rental rental, Rentaldetail rentalDetail);
}