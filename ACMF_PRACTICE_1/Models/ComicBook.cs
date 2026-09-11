using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACMF_PRACTICE_1.Models
{
    [Table("ComicBooks")]
    public class ComicBook
    {
        [Key]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(255)]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required")]
        [StringLength(255)]
        [Display(Name = "Author")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price per day is required")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, 9999999.99, ErrorMessage = "Price per day must be greater than 0")]
        [Display(Name = "Price Per Day (VND)")]
        public decimal PricePerDay { get; set; }

        public ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
