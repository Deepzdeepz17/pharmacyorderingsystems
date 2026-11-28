
using PharmacyOrdering.API.Models;

namespace PharmacyOrdering.API.Repositories.Interfaces;

public interface IOrderCancellationRepository
{
    Task<OrderCancellation?> GetByIdAsync(int cancellationId);

    Task<OrderCancellation?> GetByOrderIdAsync(int orderId);

    Task<IEnumerable<OrderCancellation>> GetAllAsync();

    Task AddAsync(OrderCancellation cancellation);

    void Update(OrderCancellation cancellation);

    Task<bool> SaveChangesAsync();
}
