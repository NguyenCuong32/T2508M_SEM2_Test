using System.ComponentModel.DataAnnotations;

namespace ComicSystemApp.DTOs
{
    public class ComicBookDTO
    {
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Tên sách không được để trống")]
        [StringLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tác giả không được để trống")]
        [StringLength(255)]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập giá thuê")]
        [Range(0, 99999999.99, ErrorMessage = "Giá thuê phải lớn hơn 0")]
        public decimal PricePerDay { get; set; }
    }
}