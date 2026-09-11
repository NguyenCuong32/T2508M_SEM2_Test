using T2508M_SEM_Test.DTOs.Rentals;
using T2508M_SEM_Test.Models;

namespace T2508M_SEM_Test.Services.Interfaces;

public interface IRentalService
{
    Task<List<Rental>> GetAllAsync();

    Task<Rental?> GetByIdAsync(int id);

    Task<(bool Success, string? ErrorMessage)> CreateAsync(
        RentalCreateDto dto);
}