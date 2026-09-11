namespace MyMvcApp.Models;

public class Rental
{
    public int RentalId { get; set; }
    public int CustomerId { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Status { get; set; } = "Đang thuê";
    public Customer Customer { get; set; } = null!;
    public List<RentalDetail> RentalDetails { get; set; } = [];
}
