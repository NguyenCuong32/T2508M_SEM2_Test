using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(255)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(15)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Registration Date")]
        [DataType(DataType.Date)]
        public DateTime Registration { get; set; } = DateTime.Now;

        public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
