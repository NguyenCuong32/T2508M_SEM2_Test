using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ComicSystem.Models;

namespace ComicSystem.ViewModels
{
    public class RentalCreateViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Vui lòng chọn khách hàng thuê.")]
        [Display(Name = "Khách hàng")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày thuê.")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày thuê")]
        public DateTime RentalDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Vui lòng chọn ngày dự kiến trả.")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày hẹn trả")]
        public DateTime ReturnDate { get; set; } = DateTime.Today.AddDays(7);

        [Required]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Đang thuê";

        public List<RentalItemInputModel> Items { get; set; } = new List<RentalItemInputModel>();

        // For UI Rendering
        public IEnumerable<SelectListItem>? CustomerList { get; set; }
        public IEnumerable<ComicBook>? AvailableBooks { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ReturnDate.Date < RentalDate.Date)
            {
                yield return new ValidationResult("Ngày hẹn trả không được trước ngày thuê.", new[] { nameof(ReturnDate) });
            }

            if (Items == null || Items.Count == 0)
            {
                yield return new ValidationResult("Vui lòng thêm ít nhất một cuốn truyện để thuê.", new[] { nameof(Items) });
            }
        }
    }
}
