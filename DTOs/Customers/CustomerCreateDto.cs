using System.ComponentModel.DataAnnotations;

namespace T2508M_SEM_Test.DTOs.Customers;

public class CustomerCreateDto
{
    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(255, ErrorMessage = "Full name cannot exceed 255 characters.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required.")]
    [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
    [Phone(ErrorMessage = "Invalid phone number.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Registration date is required.")]
    public DateTime RegistrationDate { get; set; }
}