using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace finaltest_ACMF.Models
{
    public class RentalReportItem
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

        [Display(Name = "Price/Day")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal PricePerDay { get; set; }

        [Display(Name = "Total Days")]
        public int TotalDays => Math.Max(1, (ReturnDate.Date - RentalDate.Date).Days);

        [Display(Name = "Total Amount")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal TotalAmount => TotalDays * Quantity * PricePerDay;

        [Display(Name = "Status")]
        public string Status { get; set; } = string.Empty;
    }

    public class RentalReportViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        public List<RentalReportItem> ReportItems { get; set; } = new List<RentalReportItem>();

        public int TotalQuantity => ReportItems.Sum(x => x.Quantity);
        public decimal GrandTotal => ReportItems.Sum(x => x.TotalAmount);
    }
}
