
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Customer;

// Place order request
public class CreateOrderRequest
{
    [Required]
    public int AddressId { get; set; }
}

// Order item response
public class OrderItemResponse
{
    public int MedicineId { get; set; }

    public string MedicineName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Subtotal { get; set; }
}

// Order response
public class OrderResponse
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public List<OrderItemResponse> Items { get; set; } = new();
}
