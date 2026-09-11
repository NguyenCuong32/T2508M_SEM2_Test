using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Models
{
    [Table("RentalDetails")]
    public class RentalDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentalDetailID { get; set; }

        [Required]
        public int RentalID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn truyện")]
        [Display(Name = "Truyện / Sách")]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Range(1, 100, ErrorMessage = "Số lượng phải từ 1 trở lên")]
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        [Display(Name = "Giá thuê 1 ngày")]
        public decimal PricePerDay { get; set; }

        // Navigation properties
        [ForeignKey("RentalID")]
        public virtual Rental? Rental { get; set; }

        [ForeignKey("ComicBookID")]
        public virtual ComicBook? ComicBook { get; set; }
    }
}
