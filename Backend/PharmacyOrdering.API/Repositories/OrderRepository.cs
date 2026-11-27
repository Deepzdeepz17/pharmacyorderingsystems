
using Microsoft.EntityFrameworkCore;
using PharmacyOrdering.API.Data;
using PharmacyOrdering.API.Models;
using PharmacyOrdering.API.Repositories.Interfaces;

namespace PharmacyOrdering.API.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    // Get an order by its ID
    public async Task<Order?> GetByIdAsync(int orderId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .Include(o => o.StatusHistory)
            .Include(o => o.Cancellations)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    // Get all orders belonging to a particular customer
    public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
    }

    // Get all orders
    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .ToListAsync();
    }

    // Add a new order
    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    // Update an existing order
    public void Update(Order order)
    {
        _context.Orders.Update(order);
    }

    // Save changes to the database
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
