using System;
using System.Threading.Tasks;
using ComicSystem.ViewModels;

namespace ComicSystem.Services.Interfaces
{
    public interface IReportService
    {
        Task<ReportFilterViewModel> GetRentalReportAsync(DateTime startDate, DateTime endDate);
    }
}
