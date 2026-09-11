using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACMF_Final.Domain.Entities
{
    public class ComicBook
    {
        [Key]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required")]
        [StringLength(100, ErrorMessage = "Author cannot exceed 100 characters")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price per day is required")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10,000")]
        [Display(Name = "Price Per Day")]
        public decimal PricePerDay { get; set; }
    }
}
