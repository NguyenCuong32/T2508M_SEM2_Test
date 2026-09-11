using ACMF_Final.Domain.Entities;

namespace ACMF_Final.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customers { get; }
        IRepository<ComicBook> ComicBooks { get; }
        IRepository<Rental> Rentals { get; }
        IRepository<RentalDetail> RentalDetails { get; }
        Task<int> SaveChangesAsync();
    }
}
