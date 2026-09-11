using System.ComponentModel.DataAnnotations;

namespace ComicSystemApp.DTOs
{
    public class CustomerRegisterDTO
    {
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(255)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}