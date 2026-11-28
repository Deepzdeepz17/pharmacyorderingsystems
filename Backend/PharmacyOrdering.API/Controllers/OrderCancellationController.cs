
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyOrdering.API.DTOs.Cancellations;
using PharmacyOrdering.API.Services.Interfaces;

namespace PharmacyOrdering.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderCancellationController : ControllerBase
{
    private readonly IOrderCancellationService _cancellationService;

    public OrderCancellationController(
        IOrderCancellationService cancellationService)
    {
        _cancellationService = cancellationService;
    }

    // Customer requests cancellation
    [HttpPost("{orderId:int}")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> RequestCancellation(
        int orderId,
        [FromBody] CreateCancellationDto dto)
    {
        var customerId = GetCurrentUserId();

        if (customerId == null)
            return Unauthorized();

        try
        {
            var result = await _cancellationService.RequestCancellationAsync(
                orderId, customerId.Value, dto);

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // Customer views their cancellation requests
    [HttpGet("my-cancellations")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> GetMyCancellations()
    {
        var customerId = GetCurrentUserId();

        if (customerId == null)
            return Unauthorized();

        var cancellations =
            await _cancellationService.GetMyCancellationsAsync(
                customerId.Value);

        return Ok(cancellations);
    }

    // Admin views all cancellation requests
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllCancellations()
    {
        var cancellations =
            await _cancellationService.GetAllCancellationsAsync();

        return Ok(cancellations);
    }

    // Admin approves or rejects a cancellation
    [HttpPut("{cancellationId:int}/review")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ReviewCancellation(
        int cancellationId,
        [FromBody] ReviewCancellationDto dto)
    {
        var adminId = GetCurrentUserId();

        if (adminId == null)
            return Unauthorized();

        try
        {
            var result =
                await _cancellationService.ReviewCancellationAsync(
                    cancellationId, dto, adminId.Value);

            if (!result)
                return NotFound(new
                {
                    message = "Cancellation request not found or no changes saved."
                });

            return Ok(new
            {
                message = "Cancellation request reviewed successfully."
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // Get logged-in user's ID from JWT
    private int? GetCurrentUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return int.TryParse(userId, out var id) ? id : null;
    }
}
