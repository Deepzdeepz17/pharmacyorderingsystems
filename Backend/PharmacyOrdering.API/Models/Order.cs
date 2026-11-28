
using System.ComponentModel.DataAnnotations;
using PharmacyOrdering.API.Enums;

namespace PharmacyOrdering.API.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public decimal TotalAmount { get; set; }

    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

    [Required]
    [MaxLength(255)]
    public string DeliveryAddress { get; set; } = string.Empty;

    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public ICollection<OrderItem> OrderItems { get; set; }
        = new List<OrderItem>();

    public ICollection<OrderStatusHistory> StatusHistory { get; set; }
        = new List<OrderStatusHistory>();

    public ICollection<OrderCancellation> Cancellations { get; set; }
        = new List<OrderCancellation>();
}
