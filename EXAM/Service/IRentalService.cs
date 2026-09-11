using EXAM.Models;
using EXAM.Models.ViewModels;

namespace EXAM.Service
{
    public interface IRentalService
    {
        Task<IEnumerable<Rental>> GetAllRentalsAsync();
        Task<Rental?> GetRentalByIdAsync(int id);
        Task<(bool Success, string? ErrorMessage, int RentalID)> CreateRentalAsync(CreateRentalViewModel model);
        Task<RentalReportViewModel> GetRentalReportAsync(DateTime? startDate, DateTime? endDate);
    }
}

