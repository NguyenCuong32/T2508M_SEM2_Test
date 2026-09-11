using System.ComponentModel.DataAnnotations;

namespace ACMF_PRACTICE_1.Models
{
    public class RentalReportViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        public List<RentalReportRow> Rows { get; set; } = new();
    }

    public class RentalReportRow
    {
        public int No { get; set; }
        public string BookName { get; set; } = string.Empty;
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
