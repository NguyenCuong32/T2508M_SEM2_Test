using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finaltest.Models
{
    [Table("ComicBooks")]
    public class ComicBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên truyện")]
        [StringLength(255)]
        [Display(Name = "Tên truyện (Book name)")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tên tác giả")]
        [StringLength(255)]
        [Display(Name = "Tác giả")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập giá thuê mỗi ngày")]
        [Range(0, 1000000, ErrorMessage = "Giá thuê phải lớn hơn hoặc bằng 0")]
        [Column(TypeName = "decimal(10, 2)")]
        [Display(Name = "Giá thuê / ngày (Price per day)")]
        public decimal PricePerDay { get; set; }

        // Navigation property
        public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
