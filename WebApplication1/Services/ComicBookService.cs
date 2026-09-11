using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services
{
    public class ComicBookService : IComicBookService
    {
        private readonly IComicBookRepository _comicBookRepository;

        public ComicBookService(IComicBookRepository comicBookRepository)
        {
            _comicBookRepository = comicBookRepository;
        }

        public async Task<IEnumerable<ComicBook>> GetAllComicBooksAsync(string? searchTerm = null)
        {
            return await _comicBookRepository.GetAllAsync(searchTerm);
        }

        public async Task<ComicBook?> GetComicBookByIdAsync(int id)
        {
            return await _comicBookRepository.GetByIdAsync(id);
        }

        public async Task CreateComicBookAsync(ComicBook comicBook)
        {
            await _comicBookRepository.AddAsync(comicBook);
        }

        public async Task UpdateComicBookAsync(ComicBook comicBook)
        {
            await _comicBookRepository.UpdateAsync(comicBook);
        }

        public async Task DeleteComicBookAsync(int id)
        {
            await _comicBookRepository.DeleteAsync(id);
        }

        public async Task<bool> ComicBookExistsAsync(int id)
        {
            return await _comicBookRepository.ExistsAsync(id);
        }
    }
}
