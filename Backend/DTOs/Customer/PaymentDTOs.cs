
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Customer;

// Simulated payment request
public class CreatePaymentRequest
{
    [Required]
    public int OrderId { get; set; }

    [Required]
    public string PaymentMethod { get; set; } = string.Empty;
}

// Payment response
public class PaymentResponse
{
    public int PaymentId { get; set; }

    public int OrderId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string TransactionReference { get; set; } = string.Empty;

    public DateTime PaymentDate { get; set; }
}
