using System.Collections.Generic;
using System.Threading.Tasks;
using ComicSystem.Models;
using ComicSystem.ViewModels;

namespace ComicSystem.Services.Interfaces
{
    public interface IRentalService
    {
        Task<IEnumerable<Rental>> GetAllRentalsAsync();
        Task<Rental?> GetRentalByIdAsync(int id);
        Task<(bool Success, string Message, int RentalId)> CreateRentalAsync(RentalCreateViewModel model);
        Task<bool> UpdateRentalStatusAsync(int rentalId, string status);
        Task<RentalCreateViewModel> PrepareRentalCreateViewModelAsync(RentalCreateViewModel? model = null);
    }
}
