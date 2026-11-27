
using PharmacyOrdering.API.Enums;

namespace PharmacyOrdering.API.DTOs.Cancellations;

public class CancellationResponseDto
{
    public int CancellationId { get; set; }

    public int OrderId { get; set; }

    public string Reason { get; set; } = string.Empty;

    public DateTime RequestedAt { get; set; }

    public CancellationStatus CancellationStatus { get; set; }

    public string? AdminRemarks { get; set; }
}
