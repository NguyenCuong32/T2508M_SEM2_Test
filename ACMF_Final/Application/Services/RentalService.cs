using ACMF_Final.Application.DTOs;
using ACMF_Final.Application.Interfaces;
using ACMF_Final.Domain.Entities;
using ACMF_Final.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ACMF_Final.Application.Services
{
    public class RentalService : IRentalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RentalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Rental>> GetAllAsync()
        {
            return await _unitOfWork.Rentals.Query()
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .OrderByDescending(r => r.RentalDate)
                .ToListAsync();
        }

        public async Task<Rental?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Rentals.Query()
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .FirstOrDefaultAsync(r => r.RentalID == id);
        }

        public async Task CreateRentalAsync(CreateRentalDTO dto)
        {
            var rental = new Rental
            {
                CustomerID = dto.CustomerID,
                RentalDate = dto.RentalDate,
                ReturnDate = dto.ReturnDate,
                Status = "Active"
            };

            await _unitOfWork.Rentals.AddAsync(rental);
            await _unitOfWork.SaveChangesAsync(); // Save to get RentalID

            foreach (var detail in dto.Details)
            {
                var comicBook = await _unitOfWork.ComicBooks.GetByIdAsync(detail.ComicBookID);
                if (comicBook != null)
                {
                    var rentalDetail = new RentalDetail
                    {
                        RentalID = rental.RentalID,
                        ComicBookID = detail.ComicBookID,
                        Quantity = detail.Quantity,
                        PricePerDay = comicBook.PricePerDay // Snapshot price from ComicBook
                    };
                    await _unitOfWork.RentalDetails.AddAsync(rentalDetail);
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
