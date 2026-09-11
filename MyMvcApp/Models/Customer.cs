using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models;

public class Customer
{
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ tên."), StringLength(255)]
    [Display(Name = "Họ tên")]
    public string FullName { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại."), StringLength(15)]
    [RegularExpression(@"\+?[0-9]{9,14}", ErrorMessage = "Nhập 9–14 chữ số, có thể bắt đầu bằng +.")]
    [Display(Name = "Số điện thoại")]
    public string PhoneNumber { get; set; } = "";

    [DataType(DataType.Date), Display(Name = "Ngày đăng ký")]
    [Range(typeof(DateTime), "1000-01-01", "9999-12-31", ErrorMessage = "Ngày đăng ký không hợp lệ.")]
    public DateTime RegistrationDate { get; set; } = DateTime.Today;
}
