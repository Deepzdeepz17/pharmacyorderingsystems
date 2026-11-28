
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Customer;

public class CreateAddressRequest
{
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
}

public class AddressResponse
{
    public int AddressId { get; set; }

    public string AddressLine { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public bool IsDefault { get; set; }
}


public class UpdateAddressRequest
{
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
}

