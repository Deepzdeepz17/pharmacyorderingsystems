
using PharmacyOrdering.API.DTOs.Cancellations;
using PharmacyOrdering.API.Enums;
using PharmacyOrdering.API.Models;
using PharmacyOrdering.API.Repositories.Interfaces;
using PharmacyOrdering.API.Services.Interfaces;

namespace PharmacyOrdering.API.Services;

public class OrderCancellationService : IOrderCancellationService
{
    private readonly IOrderCancellationRepository _cancellationRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderCancellationService(
        IOrderCancellationRepository cancellationRepository,
        IOrderRepository orderRepository)
    {
        _cancellationRepository = cancellationRepository;
        _orderRepository = orderRepository;
    }

    // Customer requests cancellation
    public async Task<CancellationResponseDto> RequestCancellationAsync(
        int orderId,
        int customerId,
        CreateCancellationDto dto)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        if (order == null || order.CustomerId != customerId)
            throw new InvalidOperationException("Order not found.");

        if (order.OrderStatus == OrderStatus.Delivered ||
            order.OrderStatus == OrderStatus.Cancelled)
        {
            throw new InvalidOperationException(
                "This order cannot be cancelled.");
        }

        var existingRequest =
            await _cancellationRepository.GetByOrderIdAsync(orderId);

        if (existingRequest != null)
            throw new InvalidOperationException(
                "A cancellation request already exists for this order.");

        var cancellation = new OrderCancellation
        {
            OrderId = orderId,
            RequestedBy = customerId,
            Reason = dto.Reason,
            RequestedAt = DateTime.UtcNow,
            CancellationStatus = CancellationStatus.Pending
        };

        await _cancellationRepository.AddAsync(cancellation);
        await _cancellationRepository.SaveChangesAsync();

        return new CancellationResponseDto
        {
            CancellationId = cancellation.CancellationId,
            OrderId = cancellation.OrderId,
            Reason = cancellation.Reason,
            RequestedAt = cancellation.RequestedAt,
            CancellationStatus = cancellation.CancellationStatus,
            AdminRemarks = cancellation.AdminRemarks
        };
    }

    // Customer views their cancellation requests
    public async Task<IEnumerable<CancellationResponseDto>>
        GetMyCancellationsAsync(int customerId)
    {
        var cancellations = await _cancellationRepository.GetAllAsync();

        return cancellations
            .Where(c => c.RequestedBy == customerId)
            .Select(c => new CancellationResponseDto
            {
                CancellationId = c.CancellationId,
                OrderId = c.OrderId,
                Reason = c.Reason,
                RequestedAt = c.RequestedAt,
                CancellationStatus = c.CancellationStatus,
                AdminRemarks = c.AdminRemarks
            });
    }

    // Admin reviews cancellation request
    public async Task<bool> ReviewCancellationAsync(
        int cancellationId,
        ReviewCancellationDto dto,
        int adminId)
    {
        var cancellation =
            await _cancellationRepository.GetByIdAsync(cancellationId);

        if (cancellation == null)
            return false;

        if (cancellation.CancellationStatus != CancellationStatus.Pending)
            throw new InvalidOperationException(
                "This cancellation request has already been reviewed.");

        if (dto.Status == CancellationStatus.Pending)
            throw new InvalidOperationException(
                "Admin must approve or reject the request.");

        var order = await _orderRepository.GetByIdAsync(cancellation.OrderId);

        if (order == null)
            throw new InvalidOperationException("Related order not found.");

        if (dto.Status == CancellationStatus.Approved)
        {
            if (order.OrderStatus == OrderStatus.Delivered ||
                order.OrderStatus == OrderStatus.Cancelled)
            {
                throw new InvalidOperationException(
                    "This order cannot be cancelled.");
            }

            var previousStatus = order.OrderStatus;

            order.OrderStatus = OrderStatus.Cancelled;
            order.UpdatedAt = DateTime.UtcNow;

            order.StatusHistory.Add(new OrderStatusHistory
            {
                PreviousStatus = previousStatus,
                NewStatus = OrderStatus.Cancelled,
                ChangedBy = adminId,
                ChangedAt = DateTime.UtcNow,
                Remarks = "Cancellation request approved."
            });

            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();
        }

        cancellation.CancellationStatus = dto.Status;
        cancellation.ReviewedBy = adminId;
        cancellation.ReviewedAt = DateTime.UtcNow;
        cancellation.AdminRemarks = dto.AdminRemarks;

        _cancellationRepository.Update(cancellation);

        return await _cancellationRepository.SaveChangesAsync();
    }

    // Admin views all cancellation requests
    public async Task<IEnumerable<CancellationResponseDto>>
        GetAllCancellationsAsync()
    {
        var cancellations = await _cancellationRepository.GetAllAsync();

        return cancellations.Select(c => new CancellationResponseDto
        {
            CancellationId = c.CancellationId,
            OrderId = c.OrderId,
            Reason = c.Reason,
            RequestedAt = c.RequestedAt,
            CancellationStatus = c.CancellationStatus,
            AdminRemarks = c.AdminRemarks
        });
    }
}
