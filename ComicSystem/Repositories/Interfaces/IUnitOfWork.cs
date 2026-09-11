using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace ComicSystem.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IComicBookRepository ComicBooks { get; }
        ICustomerRepository Customers { get; }
        IRentalRepository Rentals { get; }
        IGenericRepository<Models.RentalDetail> RentalDetails { get; }

        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
