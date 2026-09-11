using T2508M_SEM_Test.DTOs.Reports;

namespace T2508M_SEM_Test.Repositories.Interfaces;

public interface IReportRepository
{
    Task<List<RentalReportDto>> GetRentalReportAsync(
        DateTime startDate,
        DateTime endDate);
}