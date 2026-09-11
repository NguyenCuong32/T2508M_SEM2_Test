using ACMF_Final.Application.DTOs;
using ACMF_Final.Application.Interfaces;
using ACMF_Final.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ACMF_Final.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReportDTO>> GetRentalReportAsync(DateTime startDate, DateTime endDate)
        {
            var reportData = await _unitOfWork.RentalDetails.Query()
                .Include(rd => rd.Rental)
                    .ThenInclude(r => r.Customer)
                .Include(rd => rd.ComicBook)
                .Where(rd => rd.Rental.RentalDate >= startDate && rd.Rental.RentalDate <= endDate)
                .Select(rd => new ReportDTO
                {
                    BookName = rd.ComicBook.Title,
                    RentalDate = rd.Rental.RentalDate,
                    ReturnDate = rd.Rental.ReturnDate,
                    CustomerName = rd.Rental.Customer.FullName,
                    Quantity = rd.Quantity,
                    PricePerDay = rd.PricePerDay
                })
                .OrderBy(r => r.RentalDate)
                .ToListAsync();

            return reportData;
        }
    }
}
