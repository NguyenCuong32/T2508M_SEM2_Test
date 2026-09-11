using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Models
{
    [Table("RentalDetails")]
    public class RentalDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã chi tiết")]
        public int RentalDetailID { get; set; }

        [Required]
        [Display(Name = "Mã phiếu thuê")]
        public int RentalID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn truyện")]
        [Display(Name = "Truyện tranh")]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Range(1, 1000, ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 1")]
        [Display(Name = "Số lượng (Quantity)")]
        public int Quantity { get; set; } = 1;

        [Required(ErrorMessage = "Vui lòng nhập đơn giá thuê mỗi ngày")]
        [Range(100, 1000000, ErrorMessage = "Đơn giá 1 ngày phải lớn hơn 0")]
        [Column(TypeName = "decimal(10, 2)")]
        [Display(Name = "Giá thuê 1 ngày (PricePerDay)")]
        public decimal PricePerDay { get; set; }

        // Navigation properties
        [ForeignKey("RentalID")]
        public virtual Rental? Rental { get; set; }

        [ForeignKey("ComicBookID")]
        public virtual ComicBook? ComicBook { get; set; }
    }
}
