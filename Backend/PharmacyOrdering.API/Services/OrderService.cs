
using PharmacyOrdering.API.DTOs.Orders;
using PharmacyOrdering.API.Enums;
using PharmacyOrdering.API.Models;
using PharmacyOrdering.API.Repositories.Interfaces;
using PharmacyOrdering.API.Services.Interfaces;

namespace PharmacyOrdering.API.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    // Create a new order
    public async Task<OrderResponseDto> CreateOrderAsync(
        int customerId,
        CreateOrderDto dto)
    {
        if (dto.Items == null || !dto.Items.Any())
            throw new ArgumentException("An order must contain at least one item.");

        var orderItems = dto.Items.Select(item =>
        {
            if (item.Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            return new OrderItem
            {
                MedicineId = item.MedicineId,
                Quantity = item.Quantity
            };
        }).ToList();

        var order = new Order
        {
            CustomerId = customerId,
            OrderDate = DateTime.UtcNow,
            DeliveryAddress = dto.DeliveryAddress,
            OrderStatus = OrderStatus.Pending,
            OrderItems = orderItems,
            TotalAmount = 0
        };

        await _orderRepository.AddAsync(order);

        await _orderRepository.SaveChangesAsync();

        return new OrderResponseDto
        {
            OrderId = order.OrderId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            OrderStatus = order.OrderStatus,
            DeliveryAddress = order.DeliveryAddress
        };
    }

    // Get orders belonging to a customer
    public async Task<IEnumerable<OrderResponseDto>> GetMyOrdersAsync(
        int customerId)
    {
        var orders = await _orderRepository.GetByCustomerIdAsync(customerId);

        return orders.Select(order => new OrderResponseDto
        {
            OrderId = order.OrderId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            OrderStatus = order.OrderStatus,
            DeliveryAddress = order.DeliveryAddress
        });
    }

    // Get a particular order belonging to a customer
    public async Task<OrderDetailsDto?> GetOrderByIdAsync(
        int orderId,
        int customerId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        if (order == null || order.CustomerId != customerId)
            return null;

        return new OrderDetailsDto
        {
            OrderId = order.OrderId,
            CustomerId = order.CustomerId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            OrderStatus = order.OrderStatus,
            DeliveryAddress = order.DeliveryAddress,

            Items = order.OrderItems.Select(item => new OrderItemResponseDto
            {
                MedicineId = item.MedicineId,
                MedicineName = item.MedicineName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                SubTotal = item.SubTotal
            }).ToList()
        };
    }

    // Admin updates order status
    public async Task<bool> UpdateOrderStatusAsync(
        int orderId,
        UpdateOrderStatusDto dto,
        int adminId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        if (order == null)
            return false;

        var previousStatus = order.OrderStatus;

        if (previousStatus == dto.Status)
            return false;

        if (previousStatus == OrderStatus.Delivered ||
            previousStatus == OrderStatus.Cancelled)
            throw new InvalidOperationException(
                "This order cannot be updated.");

        order.OrderStatus = dto.Status;
        order.UpdatedAt = DateTime.UtcNow;

        order.StatusHistory.Add(new OrderStatusHistory
        {
            PreviousStatus = previousStatus,
            NewStatus = dto.Status,
            ChangedBy = adminId,
            ChangedAt = DateTime.UtcNow,
            Remarks = dto.Remarks
        });

        _orderRepository.Update(order);

        return await _orderRepository.SaveChangesAsync();
    }
}
