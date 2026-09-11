using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXAM.Models
{
    [Table("ComicBooks")]
    public class ComicBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã truyện")]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Tiêu đề truyện không được để trống")]
        [StringLength(255, ErrorMessage = "Tiêu đề truyện không được vượt quá 255 ký tự")]
        [Display(Name = "Tiêu đề truyện")]
        public string Title { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Tên tác giả không được vượt quá 255 ký tự")]
        [Display(Name = "Tác giả")]
        public string? Author { get; set; }

        [Required(ErrorMessage = "Giá thuê mỗi ngày không được để trống")]
        [Range(0, 99999999.99, ErrorMessage = "Giá thuê mỗi ngày phải là số không âm")]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Giá thuê/ngày (VNĐ)")]
        [DataType(DataType.Currency)]
        public decimal PricePerDay { get; set; }

        // Navigation property
        public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}

