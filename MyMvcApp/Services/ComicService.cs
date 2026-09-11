using MyMvcApp.Models;
using MyMvcApp.Repositories;
using MyMvcApp.ViewModels;

namespace MyMvcApp.Services;

public class ComicService(ComicRepository repository)
{
    public Task<List<ComicBook>> GetBooksAsync() => repository.GetBooksAsync();
    public Task<ComicBook?> GetBookAsync(int id) => repository.GetBookAsync(id);
    public Task<List<Customer>> GetCustomersAsync() => repository.GetCustomersAsync();
    public Task<List<Rental>> GetRentalsAsync() => repository.GetRentalsAsync();

    public async Task SaveBookAsync(ComicBook book)
    {
        book.Title = book.Title.Trim();
        book.Author = book.Author.Trim();
        if (book.ComicBookId == 0)
        {
            await repository.AddBookAsync(book);
            return;
        }

        var existing = await repository.GetBookAsync(book.ComicBookId)
            ?? throw new KeyNotFoundException("Không tìm thấy truyện.");
        existing.Title = book.Title;
        existing.Author = book.Author;
        existing.PricePerDay = book.PricePerDay;
        await repository.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await repository.GetBookAsync(id)
            ?? throw new KeyNotFoundException("Không tìm thấy truyện.");
        if (await repository.BookHasRentalsAsync(id))
            throw new InvalidOperationException("Truyện đã có lượt thuê, không thể xóa để giữ lịch sử thuê.");
        await repository.DeleteBookAsync(book);
    }

    public async Task RegisterCustomerAsync(Customer customer)
    {
        customer.CustomerId = 0;
        customer.FullName = customer.FullName.Trim();
        customer.PhoneNumber = customer.PhoneNumber.Trim();
        customer.RegistrationDate = customer.RegistrationDate.Date;
        await repository.AddCustomerAsync(customer);
    }

    public async Task<RentalCreateViewModel> GetRentalFormAsync()
    {
        var model = new RentalCreateViewModel();
        await PopulateRentalFormAsync(model);
        return model;
    }

    public async Task PopulateRentalFormAsync(RentalCreateViewModel model)
    {
        model.Customers = await repository.GetCustomersAsync();
        var books = await repository.GetBooksAsync();
        var booksById = books.ToDictionary(b => b.ComicBookId);

        foreach (var item in model.Items)
        {
            booksById.TryGetValue(item.ComicBookId, out var book);
            item.Title = book?.Title ?? "Truyện không còn tồn tại";
            item.PricePerDay = book?.PricePerDay ?? 0;
        }

        var postedIds = model.Items.Select(i => i.ComicBookId).ToHashSet();
        foreach (var book in books.Where(b => !postedIds.Contains(b.ComicBookId)))
            model.Items.Add(new RentalItemViewModel
            {
                ComicBookId = book.ComicBookId,
                Title = book.Title,
                PricePerDay = book.PricePerDay
            });
    }

    public async Task CreateRentalAsync(RentalCreateViewModel model)
    {
        if (model.RentalDate.Year < 1000 || model.ReturnDate.Year < 1000
            || model.ReturnDate.Date < model.RentalDate.Date)
            throw new ArgumentException("Ngày thuê/ngày trả không hợp lệ; ngày trả phải từ ngày thuê trở đi.");

        var selected = model.Items.Where(i => i.IsSelected).ToList();
        if (selected.Count == 0)
            throw new ArgumentException("Vui lòng chọn ít nhất một truyện.");
        if (selected.Any(i => i.Quantity < 1))
            throw new ArgumentException("Số lượng thuê phải lớn hơn 0.");
        if (selected.Select(i => i.ComicBookId).Distinct().Count() != selected.Count)
            throw new ArgumentException("Mỗi truyện chỉ được chọn một lần trong phiếu thuê.");
        if (!await repository.CustomerExistsAsync(model.CustomerId))
            throw new ArgumentException("Khách hàng không tồn tại. Vui lòng chọn lại.");

        var books = (await repository.GetBooksAsync()).ToDictionary(b => b.ComicBookId);
        if (selected.Any(i => !books.ContainsKey(i.ComicBookId)))
            throw new ArgumentException("Có truyện không còn tồn tại. Vui lòng chọn lại.");

        var rental = new Rental
        {
            CustomerId = model.CustomerId,
            RentalDate = model.RentalDate.Date,
            ReturnDate = model.ReturnDate.Date,
            Status = "Đang thuê",
            RentalDetails = selected.Select(i => new RentalDetail
            {
                ComicBookId = i.ComicBookId,
                Quantity = i.Quantity,
                PricePerDay = books[i.ComicBookId].PricePerDay
            }).ToList()
        };
        await repository.AddRentalAsync(rental);
    }

    public Task<List<RentalReportRow>> GetReportAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate.Year < 1000 || endDate.Year < 1000 || endDate.Date < startDate.Date)
            throw new ArgumentException("Khoảng ngày không hợp lệ; đến ngày phải từ ngày bắt đầu trở đi.");
        return repository.GetReportAsync(startDate.Date, endDate.Date);
    }
}
