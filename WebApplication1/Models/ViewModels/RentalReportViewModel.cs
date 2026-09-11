using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ViewModels
{
    public class RentalReportItem
    {
        public int No { get; set; }

        [Display(Name = "Bookname")]
        public string Bookname { get; set; } = string.Empty;

        [Display(Name = "Rentaldate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Rentaldate { get; set; }

        [Display(Name = "Return date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Returndate { get; set; }

        [Display(Name = "Customer name")]
        public string Customername { get; set; } = string.Empty;

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        // Extra detail fields
        public decimal PricePerDay { get; set; }
        public int RentalDays => Math.Max(1, (Returndate.Date - Rentaldate.Date).Days);
        public decimal TotalFee => RentalDays * Quantity * PricePerDay;
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

        // Summary counts
        public int TotalRentals => ReportItems.Count;
        public int TotalQuantity => ReportItems.Sum(i => i.Quantity);
        public decimal TotalRevenue => ReportItems.Sum(i => i.TotalFee);
    }
}
