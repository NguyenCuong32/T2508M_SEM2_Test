using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXAM.Models
{
    [Table("RentalDetails")]
    public class RentalDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentalDetailID { get; set; }

        [Required]
        public int RentalID { get; set; }

        [Required]
        public int ComicBookID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PricePerDay { get; set; }

        // Navigation properties
        [ForeignKey("RentalID")]
        public virtual Rental? Rental { get; set; }

        [ForeignKey("ComicBookID")]
        public virtual ComicBook? ComicBook { get; set; }
    }
}

