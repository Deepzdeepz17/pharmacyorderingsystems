
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyOrdering.API.DTOs.Orders;
using PharmacyOrdering.API.Services.Interfaces;

namespace PharmacyOrdering.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // Customer places a new order
    [HttpPost]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateOrderDto dto)
    {
        var customerId = GetCurrentUserId();//details -jwt token

        if (customerId == null)
            return Unauthorized();

        try
        {
            var result = await _orderService.CreateOrderAsync(
                customerId.Value, dto);

            return CreatedAtAction(
                nameof(GetOrderById),
                new { orderId = result.OrderId },
                result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // Customer views their own orders
    [HttpGet("my-orders")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> GetMyOrders()
    {
        var customerId = GetCurrentUserId();

        if (customerId == null)
            return Unauthorized();

        var orders = await _orderService.GetMyOrdersAsync(
            customerId.Value);

        return Ok(orders);
    }

    // Customer views a particular order
    [HttpGet("{orderId:int}")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> GetOrderById(int orderId)
    {
        var customerId = GetCurrentUserId();

        if (customerId == null)
            return Unauthorized();

        var order = await _orderService.GetOrderByIdAsync(
            orderId, customerId.Value);

        if (order == null)
            return NotFound(new { message = "Order not found." });

        return Ok(order);
    }

    // Admin updates order status
    [HttpPut("{orderId:int}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateOrderStatus(
        int orderId,
        [FromBody] UpdateOrderStatusDto dto)
    {
        var adminId = GetCurrentUserId();

        if (adminId == null)
            return Unauthorized();

        try
        {
            var updated = await _orderService.UpdateOrderStatusAsync(
                orderId, dto, adminId.Value);

            if (!updated)
                return NotFound(new { message = "Order not found or no changes saved." });

            return Ok(new { message = "Order status updated successfully." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // Read the logged-in user's ID from JWT claims
    private int? GetCurrentUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return int.TryParse(userId, out var id) ? id : null;
    }
}
