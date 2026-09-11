namespace ACMF_Final.Application.DTOs
{
    public class ReportDTO
    {
        public string BookName { get; set; } = string.Empty;
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal TotalPrice => Quantity * PricePerDay * (decimal)(ReturnDate - RentalDate).TotalDays;
    }
}
