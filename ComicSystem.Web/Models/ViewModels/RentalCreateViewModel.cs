using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComicSystem.Web.Models.ViewModels;

public class RentalCreateViewModel : IValidatableObject
{
    [Required(ErrorMessage = "Vui lòng chọn khách hàng.")]
    [Display(Name = "Khách hàng")]
    public int CustomerID { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn ngày thuê.")]
    [DataType(DataType.Date)]
    [Display(Name = "Ngày thuê")]
    public DateTime RentalDate { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Vui lòng chọn ngày trả.")]
    [DataType(DataType.Date)]
    [Display(Name = "Ngày trả")]
    public DateTime ReturnDate { get; set; } = DateTime.Today.AddDays(7);

    [Required(ErrorMessage = "Vui lòng chọn truyện tranh.")]
    [Display(Name = "Truyện tranh")]
    public int ComicBookID { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số lượng.")]
    [Range(1, 100, ErrorMessage = "Số lượng thuê từ 1 đến 100.")]
    [Display(Name = "Số lượng")]
    public int Quantity { get; set; } = 1;

    [Display(Name = "Đơn giá/ngày")]
    public decimal PricePerDay { get; set; }

    [Display(Name = "Trạng thái")]
    public string Status { get; set; } = "Đang thuê";

    // Dropdown lists
    public IEnumerable<SelectListItem>? CustomerList { get; set; }
    public IEnumerable<SelectListItem>? ComicBookList { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ReturnDate < RentalDate)
        {
            yield return new ValidationResult(
                "Ngày trả sách phải sau hoặc cùng ngày với ngày thuê.",
                new[] { nameof(ReturnDate) }
            );
        }
    }
}
