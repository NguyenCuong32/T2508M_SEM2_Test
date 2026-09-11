using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACMF_Final.Domain.Entities
{
    public class RentalDetail
    {
        [Key]
        public int RentalDetailID { get; set; }

        [Required]
        public int RentalID { get; set; }

        [Required]
        public int ComicBookID { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Price Per Day")]
        public decimal PricePerDay { get; set; }

        // Navigation properties
        public Rental Rental { get; set; } = null!;
        public ComicBook ComicBook { get; set; } = null!;
    }
}
