using System;
using System.ComponentModel.DataAnnotations;

namespace ComicSystemApp.DTOs
{
    public class RentalCreateDTO
    {
        [Required(ErrorMessage = "Vui lòng chọn khách hàng")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn truyện")]
        public int ComicBookID { get; set; }

        [Required]
        public DateTime RentalDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(3);

        [Required]
        [Range(1, 100, ErrorMessage = "Số lượng phải từ 1 trở lên")]
        public int Quantity { get; set; } = 1;
    }
}