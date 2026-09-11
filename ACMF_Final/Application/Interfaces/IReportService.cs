using ACMF_Final.Application.DTOs;

namespace ACMF_Final.Application.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDTO>> GetRentalReportAsync(DateTime startDate, DateTime endDate);
    }
}
