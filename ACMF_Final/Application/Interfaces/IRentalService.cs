using ACMF_Final.Application.DTOs;
using ACMF_Final.Domain.Entities;

namespace ACMF_Final.Application.Interfaces
{
    public interface IRentalService
    {
        Task<IEnumerable<Rental>> GetAllAsync();
        Task<Rental?> GetByIdAsync(int id);
        Task CreateRentalAsync(CreateRentalDTO dto);
    }
}
