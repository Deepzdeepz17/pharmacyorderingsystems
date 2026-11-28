
using System.ComponentModel.DataAnnotations;
using PharmacyOrdering.API.Enums;

namespace PharmacyOrdering.API.Models;

public class OrderCancellation
{
    [Key]
    public int CancellationId { get; set; }

    public int OrderId { get; set; }

    // User who requested cancellation
    public int RequestedBy { get; set; }

    [Required]
    [MaxLength(255)]
    public string Reason { get; set; } = string.Empty;

    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

    public CancellationStatus CancellationStatus { get; set; }
        = CancellationStatus.Pending;

    // Admin who reviewed the request
    public int? ReviewedBy { get; set; }

    public DateTime? ReviewedAt { get; set; }

    [MaxLength(255)]
    public string? AdminRemarks { get; set; }

    // Navigation property
    public Order? Order { get; set; }
}
