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
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên truyện")]
        [StringLength(255)]
        [Display(Name = "Tên sách / truyện")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tên tác giả")]
        [StringLength(255)]
        [Display(Name = "Tác giả")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập giá thuê 1 ngày")]
        [Range(0, 1000000, ErrorMessage = "Giá thuê phải lớn hơn hoặc bằng 0")]
        [Column(TypeName = "decimal(10, 2)")]
        [Display(Name = "Giá thuê 1 ngày (VNĐ)")]
        public decimal PricePerDay { get; set; }

        // Navigation property
        public virtual ICollection<RentalDetail>? RentalDetails { get; set; }
    }
}
