using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXAM.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã khách hàng")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(255, ErrorMessage = "Họ tên không được vượt quá 255 ký tự")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; } = string.Empty;

        [StringLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự")]
        [Display(Name = "Số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Ngày đăng ký")]
        [DataType(DataType.DateTime)]
        public DateTime? RegisterDate { get; set; }

        // Navigation property
        public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}

