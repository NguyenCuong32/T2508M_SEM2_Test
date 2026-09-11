using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComicSystem.Repositories.Interfaces;
using ComicSystem.Services.Interfaces;
using ComicSystem.ViewModels;

namespace ComicSystem.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ReportFilterViewModel> GetRentalReportAsync(DateTime startDate, DateTime endDate)
        {
            var details = await _unitOfWork.Rentals.GetRentalDetailsInDateRangeAsync(startDate, endDate);

            var reportItems = new List<ReportItemViewModel>();
            int index = 1;

            foreach (var item in details)
            {
                reportItems.Add(new ReportItemViewModel
                {
                    No = index++,
                    BookName = item.ComicBook?.Title ?? "N/A",
                    RentalDate = item.Rental?.RentalDate ?? DateTime.MinValue,
                    ReturnDate = item.Rental?.ReturnDate ?? DateTime.MinValue,
                    CustomerName = item.Rental?.Customer?.FullName ?? "N/A",
                    Quantity = item.Quantity,
                    PricePerDay = item.PricePerDay
                });
            }

            return new ReportFilterViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                Items = reportItems
            };
        }
    }
}
