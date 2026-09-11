using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicSystem.ViewModels
{
    public class ReportItemViewModel
    {
        public int No { get; set; }

        [Display(Name = "Book name")]
        public string BookName { get; set; } = string.Empty;

        [Display(Name = "Rental date")]
        [DataType(DataType.Date)]
        public DateTime RentalDate { get; set; }

        [Display(Name = "Return date")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        [Display(Name = "Customer name")]
        public string CustomerName { get; set; } = string.Empty;

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }

    public class ReportFilterViewModel
    {
        [Display(Name = "Từ ngày (Start Date)")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Đến ngày (End Date)")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public List<ReportItemViewModel> Reports { get; set; } = new List<ReportItemViewModel>();
    }
}
