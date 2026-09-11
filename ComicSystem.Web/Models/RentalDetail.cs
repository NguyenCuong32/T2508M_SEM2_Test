using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Web.Models;

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
    [Display(Name = "Mã sách")]
    public int ComicBookID { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số lượng.")]
    [Range(1, 1000, ErrorMessage = "Số lượng thuê phải từ 1 trở lên.")]
    [Display(Name = "Số lượng")]
    public int Quantity { get; set; } = 1;

    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    [Display(Name = "Đơn giá/ngày (VNĐ)")]
    [DisplayFormat(DataFormatString = "{0:N0} đ", ApplyFormatInEditMode = false)]
    public decimal PricePerDay { get; set; }

    // Navigation properties
    [ForeignKey("RentalID")]
    public virtual Rental? Rental { get; set; }

    [ForeignKey("ComicBookID")]
    public virtual ComicBook? ComicBook { get; set; }
}
