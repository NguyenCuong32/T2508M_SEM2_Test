namespace ACMF_Final.Application.DTOs
{
    public class CreateRentalDTO
    {
        public int CustomerID { get; set; }
        public DateTime RentalDate { get; set; } = DateTime.Now;
        public DateTime ReturnDate { get; set; }
        public List<RentalDetailDTO> Details { get; set; } = new List<RentalDetailDTO>();
    }
}
