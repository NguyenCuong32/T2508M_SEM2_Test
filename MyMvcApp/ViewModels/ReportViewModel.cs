using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.ViewModels;

public class ReportViewModel
{
    [DataType(DataType.Date), Display(Name = "Từ ngày")]
    public DateTime StartDate { get; set; }
    [DataType(DataType.Date), Display(Name = "Đến ngày")]
    public DateTime EndDate { get; set; }
    public List<RentalReportRow> Rows { get; set; } = [];
}

public class RentalReportRow
{
    public string BookName { get; set; } = "";
    public DateTime RentalDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string CustomerName { get; set; } = "";
    public int Quantity { get; set; }
}
