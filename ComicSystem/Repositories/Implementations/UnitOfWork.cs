using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using ComicSystem.Data;
using ComicSystem.Models;
using ComicSystem.Repositories.Interfaces;

namespace ComicSystem.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ComicSystemDbContext _context;
        private IComicBookRepository? _comicBooks;
        private ICustomerRepository? _customers;
        private IRentalRepository? _rentals;
        private IGenericRepository<RentalDetail>? _rentalDetails;
        private bool _disposed = false;

        public UnitOfWork(ComicSystemDbContext context)
        {
            _context = context;
        }

        public IComicBookRepository ComicBooks =>
            _comicBooks ??= new ComicBookRepository(_context);

        public ICustomerRepository Customers =>
            _customers ??= new CustomerRepository(_context);

        public IRentalRepository Rentals =>
            _rentals ??= new RentalRepository(_context);

        public IGenericRepository<RentalDetail> RentalDetails =>
            _rentalDetails ??= new GenericRepository<RentalDetail>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
