
using System.ComponentModel.DataAnnotations;

namespace PharmacyOrdering.API.DTOs.Orders;

public class CreateOrderItemDto
{
    [Required]
    public int MedicineId { get; set; }

    [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100.")]
    public int Quantity { get; set; }
}
