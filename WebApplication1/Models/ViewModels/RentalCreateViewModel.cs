using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models.ViewModels
{
    public class RentalCreateViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Vui lòng chọn khách hàng")]
        [Display(Name = "Khách hàng")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn truyện cần thuê")]
        [Display(Name = "Truyện tranh")]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày thuê")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày thuê (Rental date)")]
        public DateTime RentalDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Vui lòng chọn ngày trả")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày trả (Return date)")]
        public DateTime ReturnDate { get; set; } = DateTime.Today.AddDays(7);

        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Range(1, 100, ErrorMessage = "Số lượng thuê phải từ 1 đến 100 cuốn")]
        [Display(Name = "Số lượng (Quantity)")]
        public int Quantity { get; set; } = 1;

        [Required(ErrorMessage = "Vui lòng nhập đơn giá thuê")]
        [Range(0.01, 10000000, ErrorMessage = "Giá thuê phải lớn hơn 0")]
        [Display(Name = "Giá thuê / ngày (Price per day)")]
        public decimal PricePerDay { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Đang thuê";

        // Dropdown selections
        public IEnumerable<SelectListItem>? CustomerList { get; set; }
        public IEnumerable<SelectListItem>? ComicBookList { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ReturnDate < RentalDate)
            {
                yield return new ValidationResult(
                    "Ngày trả (Return date) phải lớn hơn hoặc bằng ngày thuê (Rental date).",
                    new[] { nameof(ReturnDate) }
                );
            }
        }
    }
}
