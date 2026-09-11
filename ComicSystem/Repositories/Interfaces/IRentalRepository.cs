using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicSystem.Models;

namespace ComicSystem.Repositories.Interfaces
{
    public interface IRentalRepository : IGenericRepository<Rental>
    {
        Task<IEnumerable<Rental>> GetAllWithCustomerAndDetailsAsync();
        Task<Rental?> GetWithDetailsAsync(int rentalId);
        Task<IEnumerable<RentalDetail>> GetRentalDetailsInDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
