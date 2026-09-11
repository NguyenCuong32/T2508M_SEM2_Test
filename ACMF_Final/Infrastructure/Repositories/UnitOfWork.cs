using ACMF_Final.Domain.Entities;
using ACMF_Final.Domain.Interfaces;
using ACMF_Final.Infrastructure.Data;

namespace ACMF_Final.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IRepository<Customer>? _customers;
        private IRepository<ComicBook>? _comicBooks;
        private IRepository<Rental>? _rentals;
        private IRepository<RentalDetail>? _rentalDetails;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<Customer> Customers =>
            _customers ??= new GenericRepository<Customer>(_context);

        public IRepository<ComicBook> ComicBooks =>
            _comicBooks ??= new GenericRepository<ComicBook>(_context);

        public IRepository<Rental> Rentals =>
            _rentals ??= new GenericRepository<Rental>(_context);

        public IRepository<RentalDetail> RentalDetails =>
            _rentalDetails ??= new GenericRepository<RentalDetail>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
