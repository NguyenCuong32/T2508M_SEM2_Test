using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
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
        [Display(Name = "Truyện tranh")]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Range(1, 1000, ErrorMessage = "Số lượng tối thiểu là 1")]
        [Display(Name = "Số lượng (Quantity)")]
        public int Quantity { get; set; } = 1;

        [Required(ErrorMessage = "Vui lòng nhập giá thuê mỗi ngày")]
        [Column(TypeName = "decimal(10, 2)")]
        [Display(Name = "Đơn giá thuê / ngày")]
        [DisplayFormat(DataFormatString = "{0:N0} đ", ApplyFormatInEditMode = false)]
        public decimal PricePerDay { get; set; }

        // Navigation properties
        [ForeignKey("RentalID")]
        public virtual Rental? Rental { get; set; }

        [ForeignKey("ComicBookID")]
        public virtual ComicBook? ComicBook { get; set; }
    }
}
