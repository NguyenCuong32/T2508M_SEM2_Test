using System;
using System.ComponentModel.DataAnnotations;

namespace ComicSystemApp.Models
{
    public class Rental
    {
        [Key]
        public int RentalID { get; set; }
        public int CustomerID { get; set; }
        public Customer? Customer { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        [StringLength(50)]
        public string Status { get; set; } = "Đang thuê";
    }
}