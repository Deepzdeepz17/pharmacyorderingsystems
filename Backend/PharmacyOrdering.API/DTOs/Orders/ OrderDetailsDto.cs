
using PharmacyOrdering.API.Enums;

namespace PharmacyOrdering.API.DTOs.Orders;

public class OrderDetailsDto
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public string DeliveryAddress { get; set; } = string.Empty;

    public List<OrderItemResponseDto> Items { get; set; } = new();
}
