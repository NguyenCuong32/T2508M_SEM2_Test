using System;
using System.ComponentModel.DataAnnotations;

namespace ComicSystem.ViewModels
{
    public class CustomerRegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ và tên khách hàng.")]
        [StringLength(255, ErrorMessage = "Họ tên tối đa 255 ký tự.")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [StringLength(15, ErrorMessage = "Số điện thoại tối đa 15 ký tự.")]
        [RegularExpression(@"^(\+84|0)\d{9,10}$", ErrorMessage = "Số điện thoại không đúng định dạng (ví dụ: 0912345678).")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày đăng ký")]
        public DateTime RegistrationDate { get; set; } = DateTime.Today;
    }
}
