
using PharmacyOrdering.API.Models;

namespace PharmacyOrdering.API.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int orderId);

    Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId);

    Task<IEnumerable<Order>> GetAllAsync();

    Task AddAsync(Order order);

    void Update(Order order);

    Task<bool> SaveChangesAsync();
}
