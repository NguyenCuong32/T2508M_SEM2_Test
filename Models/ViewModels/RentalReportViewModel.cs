using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models.ViewModels
{
    public class RentalReportViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Từ ngày (Start date)")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Đến ngày (End date)")]
        public DateTime? EndDate { get; set; }

        public List<RentalReportItemViewModel> ReportItems { get; set; } = new List<RentalReportItemViewModel>();

        // Summary stats
        public int TotalQuantity => ReportItems.Count > 0 ? System.Linq.Enumerable.Sum(ReportItems, x => x.Quantity) : 0;
        public decimal TotalRevenue => ReportItems.Count > 0 ? System.Linq.Enumerable.Sum(ReportItems, x => x.TotalPrice) : 0;
    }

    public class RentalReportItemViewModel
    {
        public int No { get; set; }

        [Display(Name = "Book name")]
        public string BookName { get; set; } = string.Empty;

        [Display(Name = "Rental date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RentalDate { get; set; }

        [Display(Name = "Return date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReturnDate { get; set; }

        [Display(Name = "Customer name")]
        public string CustomerName { get; set; } = string.Empty;

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Price per day")]
        public decimal PricePerDay { get; set; }

        [Display(Name = "Rental days")]
        public int RentalDays => Math.Max(1, (int)(ReturnDate.Date - RentalDate.Date).TotalDays);

        [Display(Name = "Total price")]
        public decimal TotalPrice => Quantity * PricePerDay * RentalDays;
    }
}
