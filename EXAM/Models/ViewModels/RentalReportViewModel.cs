using System.ComponentModel.DataAnnotations;

namespace EXAM.Models.ViewModels
{
    public class RentalReportItemViewModel
    {
        [Display(Name = "No")]
        public int No { get; set; }

        [Display(Name = "Book name")]
        public string BookName { get; set; } = string.Empty;

        [Display(Name = "Rental date")]
        public DateTime RentalDate { get; set; }

        [Display(Name = "Return date")]
        public DateTime? ReturnDate { get; set; }

        [Display(Name = "Customer name")]
        public string CustomerName { get; set; } = string.Empty;

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }

    public class RentalReportViewModel
    {
        [Display(Name = "Từ ngày (Start Date)")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Đến ngày (End Date)")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public List<RentalReportItemViewModel> Items { get; set; } = new List<RentalReportItemViewModel>();
    }
}

