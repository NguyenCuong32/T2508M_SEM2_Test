using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MyMvcApp.Models;

namespace MyMvcApp.ViewModels;

public class RentalCreateViewModel : IValidatableObject
{
    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn khách hàng.")]
    [Display(Name = "Khách hàng")]
    public int CustomerId { get; set; }

    [DataType(DataType.Date), Display(Name = "Ngày thuê")]
    [Range(typeof(DateTime), "1000-01-01", "9999-12-31", ErrorMessage = "Ngày thuê không hợp lệ.")]
    public DateTime RentalDate { get; set; } = DateTime.Today;

    [DataType(DataType.Date), Display(Name = "Ngày trả")]
    [Range(typeof(DateTime), "1000-01-01", "9999-12-31", ErrorMessage = "Ngày trả không hợp lệ.")]
    public DateTime ReturnDate { get; set; } = DateTime.Today.AddDays(1);

    [ValidateNever]
    public List<Customer> Customers { get; set; } = [];
    public List<RentalItemViewModel> Items { get; set; } = [];

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ReturnDate.Date < RentalDate.Date)
            yield return new ValidationResult("Ngày trả phải từ ngày thuê trở đi.", [nameof(ReturnDate)]);
        if (!Items.Any(item => item.IsSelected))
            yield return new ValidationResult("Vui lòng chọn ít nhất một truyện.");
        for (var i = 0; i < Items.Count; i++)
            if (Items[i].IsSelected && Items[i].Quantity < 1)
                yield return new ValidationResult("Số lượng thuê phải lớn hơn 0.", [$"Items[{i}].Quantity"]);
    }
}

public class RentalItemViewModel
{
    public bool IsSelected { get; set; }
    public int ComicBookId { get; set; }
    public int Quantity { get; set; } = 1;
    [ValidateNever]
    public string Title { get; set; } = "";
    [ValidateNever]
    public decimal PricePerDay { get; set; }
}
