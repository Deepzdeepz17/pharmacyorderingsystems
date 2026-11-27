
using PharmacyOrdering.API.Enums;

namespace PharmacyOrdering.API.DTOs.Orders;

public class OrderFilterDto
{
    public int? CustomerId { get; set; }

    public OrderStatus? Status { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? SearchTerm { get; set; }
}
