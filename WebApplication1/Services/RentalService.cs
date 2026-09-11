using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Repository;

namespace WebApplication1.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IComicBookRepository _comicBookRepository;

        public RentalService(IRentalRepository rentalRepository, IComicBookRepository comicBookRepository)
        {
            _rentalRepository = rentalRepository;
            _comicBookRepository = comicBookRepository;
        }

        public async Task<IEnumerable<Rental>> GetAllRentalsAsync()
        {
            return await _rentalRepository.GetAllWithDetailsAsync();
        }

        public async Task<Rental?> GetRentalByIdAsync(int id)
        {
            return await _rentalRepository.GetByIdWithDetailsAsync(id);
        }

        public async Task<Rental> RentBookAsync(RentalCreateViewModel model)
        {
            // If PricePerDay was not passed, load it from ComicBook
            if (model.PricePerDay <= 0)
            {
                var book = await _comicBookRepository.GetByIdAsync(model.ComicBookID);
                if (book != null)
                {
                    model.PricePerDay = book.PricePerDay;
                }
            }

            var rental = new Rental
            {
                CustomerID = model.CustomerID,
                RentalDate = model.RentalDate,
                ReturnDate = model.ReturnDate,
                Status = string.IsNullOrWhiteSpace(model.Status) ? "Đang thuê" : model.Status
            };

            var detail = new RentalDetail
            {
                ComicBookID = model.ComicBookID,
                Quantity = model.Quantity,
                PricePerDay = model.PricePerDay
            };

            return await _rentalRepository.CreateRentalWithDetailAsync(rental, detail);
        }

        public async Task<RentalReportViewModel> GetReportAsync(DateTime? startDate, DateTime? endDate)
        {
            var reportItems = (await _rentalRepository.GetReportAsync(startDate, endDate)).ToList();

            return new RentalReportViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                ReportItems = reportItems
            };
        }
    }
}
