namespace T2508M_SEM_Test.DTOs.Reports;

public class RentalReportDto
{
    public int No { get; set; }

    public string BookName { get; set; } = string.Empty;

    public DateTime RentalDate { get; set; }

    public DateTime ReturnDate { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public int Quantity { get; set; }
}