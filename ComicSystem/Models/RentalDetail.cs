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
        [Display(Name = "Mã đơn thuê")]
        public int RentalID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn truyện tranh.")]
        [Display(Name = "Truyện tranh")]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng.")]
        [Range(1, 1000, ErrorMessage = "Số lượng phải từ 1 đến 1000 cuốn.")]
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; } = 1;

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        [Display(Name = "Giá thuê / ngày")]
        public decimal PricePerDay { get; set; }

        // Foreign Key Navigations
        [ForeignKey("RentalID")]
        public virtual Rental? Rental { get; set; }

        [ForeignKey("ComicBookID")]
        public virtual ComicBook? ComicBook { get; set; }
    }
}
