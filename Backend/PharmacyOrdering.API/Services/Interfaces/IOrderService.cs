
using PharmacyOrdering.API.DTOs.Orders;

namespace PharmacyOrdering.API.Services.Interfaces;

public interface IOrderService
{
    Task<OrderResponseDto> CreateOrderAsync(
        int customerId,
        CreateOrderDto dto);

    Task<IEnumerable<OrderResponseDto>> GetMyOrdersAsync(
        int customerId);

    Task<OrderDetailsDto?> GetOrderByIdAsync(
        int orderId,
        int customerId);

    Task<bool> UpdateOrderStatusAsync(
        int orderId,
        UpdateOrderStatusDto dto,
        int adminId);
}
