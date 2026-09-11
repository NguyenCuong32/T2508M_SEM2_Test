using System.ComponentModel.DataAnnotations;

namespace ACMF_PRACTICE_1.Models
{
    public class RentalCreateViewModel
    {
        // Rental header
        [Required]
        [Display(Name = "Customer")]
        public int CustomerID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Rental Date")]
        public DateTime RentalDate { get; set; } = DateTime.Today;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; } = DateTime.Today.AddDays(7);

        [Display(Name = "Status")]
        public string Status { get; set; } = "Đang thuê";

        // Detail line
        [Required]
        [Display(Name = "Comic Book")]
        public int ComicBookID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; } = 1;
    }
}
