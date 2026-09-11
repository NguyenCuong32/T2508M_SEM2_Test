using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComicSystem.Models
{
    public class RentalViewModel
    {
        [Required(ErrorMessage = "Please select a customer")]
        [Display(Name = "Customer")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Please select a comic book")]
        [Display(Name = "Comic Book")]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Rental date is required")]
        [Display(Name = "Rental Date")]
        [DataType(DataType.Date)]
        public DateTime RentalDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Return date is required")]
        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; } = DateTime.Today.AddDays(7);

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 100, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; } = 1;

        public SelectList? Customers { get; set; }
        public SelectList? ComicBooks { get; set; }
    }

    public class ReportViewModel
    {
        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today.AddMonths(-1);

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Today;

        public List<ReportItem>? Results { get; set; }
    }

    public class ReportItem
    {
        public int No { get; set; }
        public string BookName { get; set; } = string.Empty;
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
