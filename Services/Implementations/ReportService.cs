using T2508M_SEM_Test.DTOs.Reports;
using T2508M_SEM_Test.Repositories.Interfaces;
using T2508M_SEM_Test.Services.Interfaces;

namespace T2508M_SEM_Test.Services.Implementations;

public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;

    public ReportService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<List<RentalReportDto>> GetRentalReportAsync(
        DateTime startDate,
        DateTime endDate)
    {
        return await _reportRepository.GetRentalReportAsync(
            startDate,
            endDate);
    }
}