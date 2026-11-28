
using Backend.DTOs.Customer;
using Backend.Services.Customer;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly PrescriptionService _prescriptionService;

    public CustomerController(PrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    // POST: api/customer/prescriptions/upload
    [HttpPost("prescriptions/upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadPrescription(
        [FromForm] PrescriptionUploadRequest request)
    {
        try
        {
            string fileName = await _prescriptionService
                .UploadPrescriptionAsync(request.File);

            return Ok(new
            {
                message = "Prescription uploaded successfully.",
                fileName = fileName
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // CART APIs

    [HttpGet("cart")]
    public IActionResult GetCart()
    {
        return Ok("Cart endpoint planned");
    }

    [HttpPost("cart/items")]
    public IActionResult AddToCart()
    {
        return Ok("Add to cart endpoint planned");
    }

    [HttpPut("cart/items/{id}")]
    public IActionResult UpdateCartItem(int id)
    {
        return Ok("Update cart endpoint planned");
    }

    [HttpDelete("cart/items/{id}")]
    public IActionResult RemoveCartItem(int id)
    {
        return Ok("Remove cart endpoint planned");
    }

    // ORDER APIs

    [HttpPost("orders")]
    public IActionResult PlaceOrder()
    {
        return Ok("Place order endpoint planned");
    }

    [HttpGet("orders")]
    public IActionResult GetOrders()
    {
        return Ok("Order history endpoint planned");
    }

    [HttpGet("orders/{id}")]
    public IActionResult GetOrderById(int id)
    {
        return Ok("Order details endpoint planned");
    }

    // PAYMENT APIs

    [HttpPost("payments")]
    public IActionResult MakePayment()
    {
        return Ok("Payment endpoint planned");
    }

    [HttpGet("payments/{orderId}")]
    public IActionResult GetPaymentByOrderId(int orderId)
    {
        return Ok("Payment history endpoint planned");
    }
}
