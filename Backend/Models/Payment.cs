using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Payment
{
    public int PaymentId { get; set; }

    // References the shared Order model
    public int OrderId { get; set; }

    public decimal Amount { get; set; }

    [Required]
    [StringLength(30)]
    public string PaymentMethod { get; set; } = string.Empty;

    [Required]
    [StringLength(30)]
    public string Status { get; set; } = "Pending";

    [StringLength(100)]
    public string? TransactionReference { get; set; }

    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
}