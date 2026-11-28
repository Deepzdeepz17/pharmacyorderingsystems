
using System.ComponentModel.DataAnnotations;
using PharmacyOrdering.API.Enums;

namespace PharmacyOrdering.API.Models;

public class OrderStatusHistory
{
    [Key]
    public int HistoryId { get; set; }

    public int OrderId { get; set; }

    public OrderStatus? PreviousStatus { get; set; }

    public OrderStatus NewStatus { get; set; }

    // User who changed the status
    public int ChangedBy { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(255)]
    public string? Remarks { get; set; }

    // Navigation property
    public Order? Order { get; set; }
}
