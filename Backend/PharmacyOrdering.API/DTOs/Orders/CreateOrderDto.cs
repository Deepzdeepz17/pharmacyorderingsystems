
using System.ComponentModel.DataAnnotations;

namespace PharmacyOrdering.API.DTOs.Orders;

public class CreateOrderDto
{
    [Required]
    [MaxLength(255)]
    public string DeliveryAddress { get; set; } = string.Empty;

    [Required]
    [MinLength(1, ErrorMessage = "Order must contain at least one medicine.")]
    public List<CreateOrderItemDto> Items { get; set; } = new();
}
