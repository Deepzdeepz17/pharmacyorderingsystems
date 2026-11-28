
using System.ComponentModel.DataAnnotations;
using PharmacyOrdering.API.Enums;

namespace PharmacyOrdering.API.DTOs.Cancellations;

public class ReviewCancellationDto
{
	[EnumDataType(typeof(CancellationStatus))]
	public CancellationStatus Status { get; set; }

	[MaxLength(255)]
	public string? AdminRemarks { get; set; }
}
