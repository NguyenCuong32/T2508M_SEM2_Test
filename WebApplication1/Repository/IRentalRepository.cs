using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Repository
{
    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetAllWithDetailsAsync();
        Task<Rental?> GetByIdWithDetailsAsync(int id);
        Task<Rental> CreateRentalWithDetailAsync(Rental rental, RentalDetail detail);
        Task<IEnumerable<RentalReportItem>> GetReportAsync(DateTime? startDate, DateTime? endDate);
    }
}
