using System.ComponentModel.DataAnnotations;
namespace Backend.DTOs.Customer;

public class CustomerProfileResponse
{
    public int CustomerId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}




public class UpdateCustomerProfileRequest
{
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 3)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Enter a valid phone number")]
    public string PhoneNumber { get; set; } = string.Empty;
}
