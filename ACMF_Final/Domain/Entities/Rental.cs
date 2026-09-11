using System.ComponentModel.DataAnnotations;

namespace ACMF_Final.Domain.Entities
{
    public class Rental
    {
        [Key]
        public int RentalID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "Rental Date")]
        [DataType(DataType.Date)]
        public DateTime RentalDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Active";

        // Navigation properties
        public Customer Customer { get; set; } = null!;
        public ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
