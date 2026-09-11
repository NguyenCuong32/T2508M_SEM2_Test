using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Web.Models;

[Table("Customers")]
public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Mã khách hàng")]
    public int CustomerID { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ tên khách hàng.")]
    [StringLength(255, ErrorMessage = "Họ tên không được vượt quá 255 ký tự.")]
    [Display(Name = "Họ tên")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    [StringLength(15, MinimumLength = 9, ErrorMessage = "Số điện thoại phải từ 9 đến 15 ký tự.")]
    [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
    [Display(Name = "Số điện thoại")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn ngày đăng ký.")]
    [DataType(DataType.DateTime)]
    [Display(Name = "Ngày đăng ký")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime RegistrationDate { get; set; } = DateTime.Now;

    // Navigation property
    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
