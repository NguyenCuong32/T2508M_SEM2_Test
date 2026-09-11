using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models;

public class ComicBook
{
    public int ComicBookId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên truyện."), StringLength(255)]
    [Display(Name = "Tên truyện")]
    public string Title { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập tác giả."), StringLength(255)]
    [Display(Name = "Tác giả")]
    public string Author { get; set; } = "";

    [Range(typeof(decimal), "0.01", "99999999.99", ErrorMessage = "Giá thuê phải từ 0.01 đến 99999999.99." )]
    [Display(Name = "Giá thuê / ngày (VNĐ)")]
    public decimal PricePerDay { get; set; }
}
