
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyOrdering.API.Models;

public class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public int MedicineId { get; set; }

    [Required]
    [MaxLength(100)]
    public string MedicineName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal SubTotal { get; set; }

    // Navigation property
    public Order? Order { get; set; }
}
