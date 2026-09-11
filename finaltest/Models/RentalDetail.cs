using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finaltest.Models
{
    [Table("RentalDetails")]
    public class RentalDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentalDetailID { get; set; }

        [Required]
        [Display(Name = "Phiếu thuê")]
        public int RentalID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn truyện tranh")]
        [Display(Name = "Truyện tranh")]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Range(1, 100, ErrorMessage = "Số lượng phải từ 1 đến 100")]
        [Display(Name = "Số lượng (Quantity)")]
        public int Quantity { get; set; } = 1;

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        [Display(Name = "Giá thuê / ngày (Price per day)")]
        public decimal PricePerDay { get; set; }

        // Navigation properties
        [ForeignKey("RentalID")]
        public virtual Rental? Rental { get; set; }

        [ForeignKey("ComicBookID")]
        public virtual ComicBook? ComicBook { get; set; }
    }
}
