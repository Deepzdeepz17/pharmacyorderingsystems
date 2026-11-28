
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyAPI.DTOs.Medicine;
using PharmacyAPI.DTOs.Stock;
using PharmacyAPI.Interfaces;

namespace PharmacyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicinesController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        // ADD MEDICINE
        [HttpPost]
        public async Task<IActionResult> CreateMedicine(CreateMedicineDto dto)
        {
            try
            {
                var result = await _medicineService.CreateMedicineAsync(dto);

                return CreatedAtAction(
                    nameof(GetMedicineById),
                    new { id = result.Id },
                    result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET ALL MEDICINES
        [HttpGet]
        public async Task<IActionResult> GetAllMedicines()
        {
            var result = await _medicineService.GetAllMedicinesAsync();

            return Ok(result);
        }

        // GET MEDICINE BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicineById(int id)
        {
            var result = await _medicineService.GetMedicineByIdAsync(id);

            if (result == null)
                return NotFound(new { message = "Medicine not found." });

            return Ok(result);
        }

        // UPDATE MEDICINE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicine(
            int id, CreateMedicineDto dto)
        {
            try
            {
                var result = await _medicineService.UpdateMedicineAsync(id, dto);

                if (result == null)
                    return NotFound(new { message = "Medicine not found." });

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE MEDICINE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var result = await _medicineService.DeleteMedicineAsync(id);

            if (!result)
                return NotFound(new { message = "Medicine not found." });

            return Ok(new
            {
                message = "Medicine deleted successfully."
            });
        }

        // UPDATE MEDICINE STOCK
        [HttpPut("{id}/stock")]
        public async Task<IActionResult> UpdateStock(
            int id, UpdateStockDto dto)
        {
            try
            {
                var result = await _medicineService.UpdateStockAsync(id, dto);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // GET ALL STOCK DETAILS
        [HttpGet("stock")]
        public async Task<IActionResult> GetStockDetails()
        {
            var result = await _medicineService.GetStockDetailsAsync();

            return Ok(result);
        }
    }
}
