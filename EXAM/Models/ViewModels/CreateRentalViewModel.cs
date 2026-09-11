using System.ComponentModel.DataAnnotations;

namespace EXAM.Models.ViewModels
{
    public class RentalBookItemInput
    {
        public int ComicBookID { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal PricePerDay { get; set; }
        public bool IsSelected { get; set; }
        public int Quantity { get; set; } = 1;
    }

    public class CreateRentalViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn khách hàng")]
        [Display(Name = "Khách hàng")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày thuê")]
        [Display(Name = "Ngày thuê")]
        [DataType(DataType.Date)]
        public DateTime RentalDate { get; set; } = DateTime.Today;

        [Display(Name = "Ngày dự kiến trả")]
        [DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; } = DateTime.Today.AddDays(7);

        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Đang thuê";

        public List<RentalBookItemInput> Books { get; set; } = new List<RentalBookItemInput>();
    }
}

