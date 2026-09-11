using T2508M_SEM_Test.DTOs.Reports;

namespace T2508M_SEM_Test.Services.Interfaces;

public interface IReportService
{
    Task<List<RentalReportDto>> GetRentalReportAsync(
        DateTime startDate,
        DateTime endDate);
}