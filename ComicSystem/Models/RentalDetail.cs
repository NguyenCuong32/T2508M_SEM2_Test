using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Models
{
    public class RentalDetail
    {
        [Key]
        public int RentalDetailID { get; set; }

        [Required]
        public int RentalID { get; set; }

        [Required]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 100, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Price Per Day")]
        public decimal PricePerDay { get; set; }

        [ForeignKey("RentalID")]
        public virtual Rental? Rental { get; set; }

        [ForeignKey("ComicBookID")]
        public virtual ComicBook? ComicBook { get; set; }
    }
}
