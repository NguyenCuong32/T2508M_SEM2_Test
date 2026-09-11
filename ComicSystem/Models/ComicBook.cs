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

        [Required(ErrorMessage = "Vui lòng nhập tên truyện tranh.")]
        [StringLength(255, ErrorMessage = "Tên truyện không được vượt quá 255 ký tự.")]
        [Display(Name = "Tên sách")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tên tác giả.")]
        [StringLength(255, ErrorMessage = "Tên tác giả không được vượt quá 255 ký tự.")]
        [Display(Name = "Tác giả")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập giá thuê mỗi ngày.")]
        [Range(0.01, 10000000.0, ErrorMessage = "Giá thuê phải lớn hơn 0.")]
        [Column(TypeName = "decimal(10, 2)")]
        [DataType(DataType.Currency)]
        [Display(Name = "Giá thuê / ngày (VNĐ)")]
        public decimal PricePerDay { get; set; }

        // Navigation property
        public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
