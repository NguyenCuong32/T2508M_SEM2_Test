using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComicSystem.Models.ViewModels
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
        [Range(1, 100, ErrorMessage = "Số lượng phải từ 1 đến 100 cuốn")]
        [Display(Name = "Số lượng (Quantity)")]
        public int Quantity { get; set; } = 1;

        [Required(ErrorMessage = "Vui lòng nhập giá thuê mỗi ngày")]
        [Range(100, 1000000, ErrorMessage = "Giá thuê mỗi ngày phải từ 100 đ")]
        [Display(Name = "Giá thuê 1 ngày (Price per day)")]
        public decimal PricePerDay { get; set; }

        [Display(Name = "Trạng thái (Status)")]
        public string Status { get; set; } = "Đang thuê";

        // Dropdown lists
        public SelectList? CustomersList { get; set; }
        public SelectList? ComicBooksList { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ReturnDate < RentalDate)
            {
                yield return new ValidationResult("Ngày trả không được sớm hơn ngày thuê.", new[] { nameof(ReturnDate) });
            }
        }
    }
}
