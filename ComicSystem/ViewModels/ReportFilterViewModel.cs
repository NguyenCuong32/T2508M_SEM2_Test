using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ComicSystem.ViewModels
{
    public class ReportFilterViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn ngày bắt đầu.")]
        [DataType(DataType.Date)]
        [Display(Name = "Từ ngày")]
        public DateTime StartDate { get; set; } = new DateTime(2024, 10, 1);

        [Required(ErrorMessage = "Vui lòng chọn ngày kết thúc.")]
        [DataType(DataType.Date)]
        [Display(Name = "Đến ngày")]
        public DateTime EndDate { get; set; } = DateTime.Today;

        public List<ReportItemViewModel> Items { get; set; } = new List<ReportItemViewModel>();

        // Summary metrics
        public int TotalRecords => Items.Count;
        public int TotalQuantity => Items.Sum(i => i.Quantity);
        public decimal TotalEstimatedRevenue => Items.Sum(i => i.EstimatedTotal);
    }
}
