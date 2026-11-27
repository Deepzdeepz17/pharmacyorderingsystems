
using System.ComponentModel.DataAnnotations;
using PharmacyOrdering.API.Enums;

namespace PharmacyOrdering.API.DTOs.Orders;

public class UpdateOrderStatusDto
{
    [EnumDataType(typeof(OrderStatus))]
    public OrderStatus Status { get; set; }

    [MaxLength(255)]
    public string? Remarks { get; set; }
}
