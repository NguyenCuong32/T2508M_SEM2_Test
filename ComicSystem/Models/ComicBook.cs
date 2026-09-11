using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Models
{
    public class ComicBook
    {
        [Key]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required")]
        [StringLength(255)]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price per day is required")]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Price Per Day")]
        [Range(0.01, 9999999.99, ErrorMessage = "Price must be greater than 0")]
        public decimal PricePerDay { get; set; }

        public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
