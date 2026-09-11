using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Models
{
    [Table("ComicBooks")]
    public class ComicBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã truyện")]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên truyện tranh")]
        [StringLength(255, ErrorMessage = "Tên sách không được vượt quá 255 ký tự")]
        [Display(Name = "Tên sách (Title)")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tên tác giả")]
        [StringLength(255, ErrorMessage = "Tác giả không được vượt quá 255 ký tự")]
        [Display(Name = "Tác giả (Author)")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập giá thuê mỗi ngày")]
        [Range(100, 1000000, ErrorMessage = "Giá thuê 1 ngày phải lớn hơn 0 (từ 100 VNĐ)")]
        [Column(TypeName = "decimal(10, 2)")]
        [Display(Name = "Giá thuê 1 ngày (PricePerDay)")]
        [DataType(DataType.Currency)]
        public decimal PricePerDay { get; set; }

        // Navigation property
        public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
