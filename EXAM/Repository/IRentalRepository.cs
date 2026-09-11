using EXAM.Models;
using EXAM.Models.ViewModels;

namespace EXAM.Repository
{
    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetAllRentalsAsync();
        Task<Rental?> GetRentalByIdAsync(int id);
        Task<int> CreateRentalWithDetailsAsync(Rental rental, IEnumerable<RentalDetail> details);
        Task<List<RentalReportItemViewModel>> GetRentalReportAsync(DateTime? startDate, DateTime? endDate);
    }
}

