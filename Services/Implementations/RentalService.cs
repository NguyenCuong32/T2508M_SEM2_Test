using T2508M_SEM_Test.DTOs.Rentals;
using T2508M_SEM_Test.Models;
using T2508M_SEM_Test.Repositories.Interfaces;
using T2508M_SEM_Test.Services.Interfaces;

namespace T2508M_SEM_Test.Services.Implementations;

public class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IComicBookRepository _comicBookRepository;

    public RentalService(
        IRentalRepository rentalRepository,
        ICustomerRepository customerRepository,
        IComicBookRepository comicBookRepository)
    {
        _rentalRepository = rentalRepository;
        _customerRepository = customerRepository;
        _comicBookRepository = comicBookRepository;
    }

    public async Task<List<Rental>> GetAllAsync()
    {
        return await _rentalRepository.GetAllAsync();
    }

    public async Task<Rental?> GetByIdAsync(int id)
    {
        return await _rentalRepository.GetByIdAsync(id);
    }

    public async Task<(bool Success, string? ErrorMessage)> CreateAsync(
        RentalCreateDto dto)
    {
        if (dto.ReturnDate < dto.RentalDate)
        {
            return (
                false,
                "Return date cannot be earlier than rental date."
            );
        }

        if (dto.Quantity <= 0)
        {
            return (
                false,
                "Quantity must be greater than 0."
            );
        }

        var customer =
            await _customerRepository.GetByIdAsync(dto.CustomerId);

        if (customer == null)
        {
            return (
                false,
                "Selected customer does not exist."
            );
        }

        var comicBook =
            await _comicBookRepository.GetByIdAsync(dto.ComicBookId);

        if (comicBook == null)
        {
            return (
                false,
                "Selected comic book does not exist."
            );
        }

        var rental = new Rental
        {
            CustomerId = dto.CustomerId,
            RentalDate = dto.RentalDate,
            ReturnDate = dto.ReturnDate,
            Status = "Active"
        };

        var rentalDetail = new Rentaldetail
        {
            ComicBookId = dto.ComicBookId,
            Quantity = dto.Quantity,

            // Store the current comic book price
            PricePerDay = comicBook.PricePerDay
        };

        await _rentalRepository.CreateRentalAsync(
            rental,
            rentalDetail);

        return (true, null);
    }
}