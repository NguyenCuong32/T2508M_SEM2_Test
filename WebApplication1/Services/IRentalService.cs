using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Services
{
    public interface IRentalService
    {
        Task<IEnumerable<Rental>> GetAllRentalsAsync();
        Task<Rental?> GetRentalByIdAsync(int id);
        Task<Rental> RentBookAsync(RentalCreateViewModel model);
        Task<RentalReportViewModel> GetReportAsync(DateTime? startDate, DateTime? endDate);
    }
}
