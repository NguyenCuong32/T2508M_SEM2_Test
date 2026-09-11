using System.ComponentModel.DataAnnotations;

namespace finaltest.Models
{
    public class RentalReportItemViewModel
    {
        public int No { get; set; }
        public string BookName { get; set; } = string.Empty;
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PricePerDay { get; set; }
        public int TotalDays => Math.Max(1, (ReturnDate.Date - RentalDate.Date).Days);
        public decimal TotalFee => TotalDays * PricePerDay * Quantity;
    }

    public class RentalReportViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Từ ngày (Start date)")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Đến ngày (End date)")]
        public DateTime? EndDate { get; set; }

        public List<RentalReportItemViewModel> Items { get; set; } = new List<RentalReportItemViewModel>();

        public int TotalQuantity => Items.Sum(x => x.Quantity);
        public decimal GrandTotalFee => Items.Sum(x => x.TotalFee);
    }
}
