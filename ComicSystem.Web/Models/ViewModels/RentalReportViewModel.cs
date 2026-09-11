using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Web.Models.ViewModels;

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
}

public class RentalReportViewModel
{
    [Display(Name = "Từ ngày")]
    [DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }

    [Display(Name = "Đến ngày")]
    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    public List<RentalReportItemViewModel> Items { get; set; } = new();

    public int TotalRentals => Items.Count;
    public int TotalQuantity => Items.Sum(i => i.Quantity);
}
