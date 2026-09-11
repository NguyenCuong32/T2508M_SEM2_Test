using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class RentalDetail 
    {
        [Key]
        public int RentalDetailId { get; set; }
        [Required]
        public int RentalId { get; set; }
        [Required]
        public int ComicBookId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal PricePerDay { get; set; }

        [ForeignKey("RentalId")]
        public Rental Rental { get; set; }

        [ForeignKey("ComicBookId")]
        public ComicBooks ComicBook { get; set; }
    }
}
