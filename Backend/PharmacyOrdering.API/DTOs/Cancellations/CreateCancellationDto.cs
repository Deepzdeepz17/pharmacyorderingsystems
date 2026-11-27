
using System.ComponentModel.DataAnnotations;

namespace PharmacyOrdering.API.DTOs.Cancellations;

public class CreateCancellationDto
{
    [Required]
    [MaxLength(255)]
    public string Reason { get; set; } = string.Empty;
}
