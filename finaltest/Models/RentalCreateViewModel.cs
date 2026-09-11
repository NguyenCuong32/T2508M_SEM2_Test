using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace finaltest.Models
{
    public class RentalCreateViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn khách hàng")]
        [Display(Name = "Khách hàng")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày thuê")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày thuê (Rental date)")]
        public DateTime RentalDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Vui lòng chọn ngày trả")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày trả (Return date)")]
        public DateTime ReturnDate { get; set; } = DateTime.Today.AddDays(7);

        [Required]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Đang thuê";

        [Required(ErrorMessage = "Vui lòng chọn truyện tranh")]
        [Display(Name = "Truyện tranh (Comic Book)")]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Range(1, 100, ErrorMessage = "Số lượng phải từ 1 đến 100")]
        [Display(Name = "Số lượng (Quantity)")]
        public int Quantity { get; set; } = 1;

        [Display(Name = "Giá thuê / ngày (Price per day)")]
        public decimal PricePerDay { get; set; }

        // Dropdown select lists
        public SelectList? CustomersList { get; set; }
        public SelectList? ComicBooksList { get; set; }
    }
}
