using System;
using System.ComponentModel.DataAnnotations;

namespace finaltest_ACMF.Models
{
    public class RentalCreateViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Vui lòng chọn khách hàng.")]
        [Display(Name = "Khách hàng thuê")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn truyện.")]
        [Display(Name = "Truyện muốn thuê")]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày thuê.")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày thuê")]
        public DateTime RentalDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Vui lòng chọn ngày trả.")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày hẹn trả")]
        public DateTime ReturnDate { get; set; } = DateTime.Today.AddDays(7);

        [Required(ErrorMessage = "Vui lòng nhập số lượng.")]
        [Range(1, 100, ErrorMessage = "Số lượng truyện phải từ 1 đến 100.")]
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; } = 1;

        [Display(Name = "Đơn giá thuê / ngày")]
        public decimal? PricePerDay { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Đang thuê";

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ReturnDate < RentalDate)
            {
                yield return new ValidationResult(
                    "Ngày hẹn trả không được nhỏ hơn ngày thuê.",
                    new[] { nameof(ReturnDate) });
            }
        }
    }
}
