using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class CartItem
{
    public int CartItemId { get; set; }

    public int CartId { get; set; }

    // References the shared Medicine model
    public int MedicineId { get; set; }

    [Range(1, 100)]
    public int Quantity { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}