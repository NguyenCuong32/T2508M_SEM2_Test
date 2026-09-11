using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACMF_PRACTICE_1.Models
{
    [Table("RentalDetails")]
    public class RentalDetail
    {
        [Key]
        public int RentalDetailID { get; set; }

        [Required]
        [Display(Name = "Rental")]
        public int RentalID { get; set; }

        [Required]
        [Display(Name = "Comic Book")]
        public int ComicBookID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Price Per Day")]
        public decimal PricePerDay { get; set; }

        [ForeignKey("RentalID")]
        public Rental? Rental { get; set; }

        [ForeignKey("ComicBookID")]
        public ComicBook? ComicBook { get; set; }
    }
}
