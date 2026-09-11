using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("ComicBooks")]
    public class ComicBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên truyện")]
        [StringLength(255, ErrorMessage = "Tên truyện không được quá 255 ký tự")]
        [Display(Name = "Tên truyện (Title)")]
        public string Title { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Tên tác giả không được quá 255 ký tự")]
        [Display(Name = "Tác giả (Author)")]
        public string? Author { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá thuê mỗi ngày")]
        [Range(0.01, 10000000, ErrorMessage = "Giá thuê phải lớn hơn 0")]
        [Column(TypeName = "decimal(10, 2)")]
        [Display(Name = "Giá thuê / ngày (PricePerDay)")]
        [DisplayFormat(DataFormatString = "{0:N0} đ", ApplyFormatInEditMode = false)]
        public decimal PricePerDay { get; set; }

        // Navigation property
        public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
