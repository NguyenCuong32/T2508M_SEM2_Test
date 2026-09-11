using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class ComicBooks
    {

        [Key]
        public int ComicBookId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [Required]
        [StringLength(255)]
        public string Author { get; set; }
        [Required]
        public decimal PricePerDay { get; set; }

        public ICollection<RentalDetail> RentalDetails { get; set; }
    }
}