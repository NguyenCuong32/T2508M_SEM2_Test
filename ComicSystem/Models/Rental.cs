using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Models
{
    public class Rental
    {
        [Key]
        public int RentalID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Rental date is required")]
        [Display(Name = "Rental Date")]
        [DataType(DataType.Date)]
        public DateTime RentalDate { get; set; }

        [Required(ErrorMessage = "Return date is required")]
        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Active";

        [ForeignKey("CustomerID")]
        public virtual Customer? Customer { get; set; }

        public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
