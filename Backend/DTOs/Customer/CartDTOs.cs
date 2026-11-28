
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Customer;

// Add medicine to cart
public class AddToCartRequest
{
    [Required]
    public int MedicineId { get; set; }

    [Range(1, 100)]
    public int Quantity { get; set; }
}

// Update cart item quantity
public class UpdateCartItemRequest
{
    [Range(1, 100)]
    public int Quantity { get; set; }
}

// Individual cart item response
public class CartItemResponse
{
    public int MedicineId { get; set; }

    public string MedicineName { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal Subtotal { get; set; }
}

// Complete cart response
public class CartResponse
{
    public List<CartItemResponse> Items { get; set; } = new();

    public decimal TotalAmount { get; set; }
}
