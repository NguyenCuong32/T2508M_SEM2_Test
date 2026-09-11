using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ACMF_Final.Web.ViewModels
{
    public class CreateRentalViewModel
    {
        [Required(ErrorMessage = "Please select a customer")]
        [Display(Name = "Customer")]
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "Rental Date")]
        [DataType(DataType.Date)]
        public DateTime RentalDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(7);

        public List<RentalItemViewModel> Items { get; set; } = new List<RentalItemViewModel>();

        // For dropdowns
        public SelectList? CustomerList { get; set; }
        public List<ACMF_Final.Domain.Entities.ComicBook>? AvailableComics { get; set; }
    }
}
