using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class CustomerAddress
{
    public int AddressId { get; set; }

    // Reference to the shared Users table
    public int UserId { get; set; }

    [Required]
    [StringLength(100)]
    public string AddressLine { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string City { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string State { get; set; } = string.Empty;

    [Required]
    [StringLength(10)]
    public string PostalCode { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Country { get; set; } = "India";

    public bool IsDefault { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}