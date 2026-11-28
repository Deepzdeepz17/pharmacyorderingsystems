
using Microsoft.EntityFrameworkCore;
using PharmacyOrdering.API.Data;
using PharmacyOrdering.API.Models;
using PharmacyOrdering.API.Repositories.Interfaces;

namespace PharmacyOrdering.API.Repositories;

public class OrderCancellationRepository : IOrderCancellationRepository
{
    private readonly AppDbContext _context;

    public OrderCancellationRepository(AppDbContext context)
    {
        _context = context;
    }

    // Get cancellation request by its ID
    public async Task<OrderCancellation?> GetByIdAsync(int cancellationId)
    {
        return await _context.OrderCancellations
            .Include(c => c.Order)
            .FirstOrDefaultAsync(c => c.CancellationId == cancellationId);
    }

    // Get cancellation request for a particular order
    public async Task<OrderCancellation?> GetByOrderIdAsync(int orderId)
    {
        return await _context.OrderCancellations
            .Include(c => c.Order)
            .FirstOrDefaultAsync(c => c.OrderId == orderId);
    }

    // Get all cancellation requests
    public async Task<IEnumerable<OrderCancellation>> GetAllAsync()
    {
        return await _context.OrderCancellations
            .Include(c => c.Order)
            .ToListAsync();
    }

    // Add a new cancellation request
    public async Task AddAsync(OrderCancellation cancellation)
    {
        await _context.OrderCancellations.AddAsync(cancellation);
    }

    // Update cancellation request
    public void Update(OrderCancellation cancellation)
    {
        _context.OrderCancellations.Update(cancellation);
    }

    // Save changes to the database
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
