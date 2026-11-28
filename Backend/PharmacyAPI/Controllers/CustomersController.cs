
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyAPI.DTOs.Customer;
using PharmacyAPI.Interfaces;
using PharmacyAPI.Services;

namespace PharmacyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET ALL CUSTOMERS
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result =
                await _customerService.GetAllCustomersAsync();

            return Ok(result);
        }

        // GET CUSTOMER BY ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var result =
                await _customerService.GetCustomerByIdAsync(id);

            if (result == null)
                return NotFound(new
                {
                    message = "Customer not found."
                });

            return Ok(result);
        }

        // SEARCH CUSTOMERS
        [HttpGet("search")]
        public async Task<IActionResult> SearchCustomers(
            [FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return BadRequest(new
                {
                    message = "Search term is required."
                });

            var result =
                await _customerService.SearchCustomersAsync(searchTerm.Trim());

            return Ok(result);
        }

        // ACTIVATE OR DEACTIVATE CUSTOMER
        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateCustomerStatus(
            int id, CustomerStatusDto dto)
        {
            var result =
                await _customerService.UpdateCustomerStatusAsync(id, dto);

            if (result == null)
                return NotFound(new
                {
                    message = "Customer not found."
                });

            return Ok(result);
        }
    }
}
