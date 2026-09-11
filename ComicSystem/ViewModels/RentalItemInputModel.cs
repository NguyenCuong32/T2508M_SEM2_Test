using System.ComponentModel.DataAnnotations;

namespace ComicSystem.ViewModels
{
    public class RentalItemInputModel
    {
        [Required(ErrorMessage = "Vui lòng chọn truyện tranh.")]
        [Display(Name = "Truyện tranh")]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng.")]
        [Range(1, 1000, ErrorMessage = "Số lượng phải từ 1 đến 1000.")]
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; } = 1;

        [Display(Name = "Giá thuê / ngày")]
        public decimal PricePerDay { get; set; }
    }
}
