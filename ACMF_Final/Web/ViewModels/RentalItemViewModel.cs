namespace ACMF_Final.Web.ViewModels
{
    public class RentalItemViewModel
    {
        public int ComicBookID { get; set; }
        public bool IsSelected { get; set; }
        public int Quantity { get; set; } = 1;

        // Display info
        public string? Title { get; set; }
        public string? Author { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
