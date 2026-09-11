using System.ComponentModel.DataAnnotations;

namespace T2508M_SEM_Test.DTOs.Rentals;

public class RentalCreateDto
{
    [Required(ErrorMessage = "Customer is required.")]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Comic book is required.")]
    public int ComicBookId { get; set; }

    [Required(ErrorMessage = "Rental date is required.")]
    public DateTime RentalDate { get; set; }

    [Required(ErrorMessage = "Return date is required.")]
    public DateTime ReturnDate { get; set; }

    [Range(1, 999, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; } = 1;
}