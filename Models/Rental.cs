using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Rental
    {
        [Key]
        public int RentalId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        [ForeignKey("CustomerId")]
        public Customers Customer { get; set; }
        public ICollection<RentalDetail> RentalDetails { get; set; }
    }
}
