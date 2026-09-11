using System.ComponentModel.DataAnnotations;

namespace T2508M_SEM_Test.DTOs.ComicBooks;

public class ComicBookCreateDto
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(255, ErrorMessage = "Title cannot exceed 255 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Author is required.")]
    [StringLength(255, ErrorMessage = "Author cannot exceed 255 characters.")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "Price per day is required.")]
    [Range(0.01, 999999999, ErrorMessage = "Price per day must be greater than 0.")]
    public decimal PricePerDay { get; set; }
}