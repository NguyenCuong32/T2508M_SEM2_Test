using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finaltest_ACMF.Models
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

        [Required(ErrorMessage = "Vui lòng chọn truyện.")]
        [Display(Name = "Truyện")]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng.")]
        [Range(1, 100, ErrorMessage = "Số lượng phải từ 1 đến 100 quyển.")]
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; } = 1;

        [Required(ErrorMessage = "Vui lòng nhập đơn giá thuê.")]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Giá thuê / ngày (VNĐ)")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public decimal PricePerDay { get; set; }

        // Navigation Properties
        [ForeignKey("RentalID")]
        public virtual Rental? Rental { get; set; }

        [ForeignKey("ComicBookID")]
        public virtual ComicBook? ComicBook { get; set; }
    }
}
