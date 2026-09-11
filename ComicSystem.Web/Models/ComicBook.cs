using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Web.Models;

[Table("ComicBooks")]
public class ComicBook
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Mã sách")]
    public int ComicBookID { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên truyện.")]
    [StringLength(255, ErrorMessage = "Tên truyện không được vượt quá 255 ký tự.")]
    [Display(Name = "Tên sách")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập tên tác giả.")]
    [StringLength(255, ErrorMessage = "Tác giả không được vượt quá 255 ký tự.")]
    [Display(Name = "Tác giả")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập giá thuê mỗi ngày.")]
    [Range(0.01, 10000000.00, ErrorMessage = "Giá thuê mỗi ngày phải lớn hơn 0.")]
    [Column(TypeName = "decimal(10, 2)")]
    [Display(Name = "Giá thuê 1 ngày (VNĐ)")]
    [DisplayFormat(DataFormatString = "{0:N0} đ", ApplyFormatInEditMode = false)]
    public decimal PricePerDay { get; set; }

    // Navigation property
    public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
}
