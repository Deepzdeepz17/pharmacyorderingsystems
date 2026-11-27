
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyAPI.DTOs.Medicine;
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

        // Add Medicine
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

        // Get All Medicines
        [HttpGet]
        public async Task<IActionResult> GetAllMedicines()
        {
            var result = await _medicineService.GetAllMedicinesAsync();
            return Ok(result);
        }

        // Get Medicine By ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicineById(int id)
        {
            var result = await _medicineService.GetMedicineByIdAsync(id);

            if (result == null)
                return NotFound(new { message = "Medicine not found." });

            return Ok(result);
        }

        // Update Medicine
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

        // Delete Medicine
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var result = await _medicineService.DeleteMedicineAsync(id);

            if (!result)
                return NotFound(new { message = "Medicine not found." });

            return Ok(new { message = "Medicine deleted successfully." });
        }
    }
}
