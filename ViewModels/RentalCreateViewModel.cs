using System;
using System.ComponentModel.DataAnnotations;

namespace ComicSystem.ViewModels
{
    public class RentalCreateViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn khách hàng")]
        [Display(Name = "Tên khách hàng")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn sách / truyện")]
        [Display(Name = "Tên truyện")]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày thuê")]
        [Display(Name = "Ngày thuê")]
        [DataType(DataType.Date)]
        public DateTime RentalDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Vui lòng chọn ngày trả")]
        [Display(Name = "Ngày trả")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; } = DateTime.Today.AddDays(7);

        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Range(1, 100, ErrorMessage = "Số lượng từ 1 trở lên")]
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; } = 1;

        [Display(Name = "Giá thuê 1 ngày (VNĐ)")]
        public decimal PricePerDay { get; set; }
    }
}
