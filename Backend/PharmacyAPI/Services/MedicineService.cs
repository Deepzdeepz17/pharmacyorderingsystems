
using PharmacyAPI.DTOs.Medicine;
using PharmacyAPI.DTOs.Stock;
using PharmacyAPI.Interfaces;
using PharmacyAPI.Models;

namespace PharmacyAPI.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly ICategoryRepository _categoryRepository;

        public MedicineService(
            IMedicineRepository medicineRepository,
            ICategoryRepository categoryRepository)
        {
            _medicineRepository = medicineRepository;
            _categoryRepository = categoryRepository;
        }

        // CREATE MEDICINE
        public async Task<MedicineResponseDto> CreateMedicineAsync(
            CreateMedicineDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

            if (category == null)
                throw new ArgumentException("Category not found.");

            var medicine = new Medicine
            {
                Name = dto.Name.Trim(),
                Description = dto.Description?.Trim(),
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                ImageUrl = dto.ImageUrl,
                IsPrescriptionRequired = dto.IsPrescriptionRequired,
                IsAvailable = dto.IsAvailable,
                CategoryId = dto.CategoryId
            };

            var createdMedicine = await _medicineRepository.AddAsync(medicine);

            return MapToDto(createdMedicine, category.Name);
        }

        // GET ALL MEDICINES
        public async Task<List<MedicineResponseDto>> GetAllMedicinesAsync()
        {
            var medicines = await _medicineRepository.GetAllAsync();

            return medicines.Select(m =>
                MapToDto(m, m.Category?.Name)).ToList();
        }

        // GET MEDICINE BY ID
        public async Task<MedicineResponseDto?> GetMedicineByIdAsync(int id)
        {
            var medicine = await _medicineRepository.GetByIdAsync(id);

            if (medicine == null)
                return null;

            return MapToDto(medicine, medicine.Category?.Name);
        }

        // UPDATE MEDICINE
        public async Task<MedicineResponseDto?> UpdateMedicineAsync(
            int id, CreateMedicineDto dto)
        {
            var medicine = await _medicineRepository.GetByIdAsync(id);

            if (medicine == null)
                return null;

            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

            if (category == null)
                throw new ArgumentException("Category not found.");

            medicine.Name = dto.Name.Trim();
            medicine.Description = dto.Description?.Trim();
            medicine.Price = dto.Price;
            medicine.StockQuantity = dto.StockQuantity;
            medicine.ImageUrl = dto.ImageUrl;
            medicine.IsPrescriptionRequired = dto.IsPrescriptionRequired;
            medicine.IsAvailable = dto.IsAvailable;
            medicine.CategoryId = dto.CategoryId;

            await _medicineRepository.UpdateAsync(medicine);

            return MapToDto(medicine, category.Name);
        }

        // DELETE MEDICINE
        public async Task<bool> DeleteMedicineAsync(int id)
        {
            var medicine = await _medicineRepository.GetByIdAsync(id);

            if (medicine == null)
                return false;

            await _medicineRepository.DeleteAsync(medicine);

            return true;
        }

        // UPDATE MEDICINE STOCK
        public async Task<StockResponseDto> UpdateStockAsync(
            int medicineId, UpdateStockDto dto)
        {
            var medicine = await _medicineRepository.GetByIdAsync(medicineId);

            if (medicine == null)
                throw new ArgumentException("Medicine not found.");

            medicine.StockQuantity = dto.StockQuantity;

            medicine.IsAvailable = dto.StockQuantity > 0;

            await _medicineRepository.UpdateAsync(medicine);

            return MapToStockDto(medicine);
        }

        // GET STOCK DETAILS
        public async Task<List<StockResponseDto>> GetStockDetailsAsync()
        {
            var medicines = await _medicineRepository.GetAllAsync();

            return medicines.Select(MapToStockDto).ToList();
        }

        // MAP MEDICINE TO STOCK RESPONSE DTO
        private static StockResponseDto MapToStockDto(Medicine medicine)
        {
            string status;

            if (medicine.StockQuantity == 0)
                status = "Out of Stock";
            else if (medicine.StockQuantity <= 10)
                status = "Low Stock";
            else
                status = "In Stock";

            return new StockResponseDto
            {
                MedicineId = medicine.Id,
                MedicineName = medicine.Name,
                StockQuantity = medicine.StockQuantity,
                IsAvailable = medicine.IsAvailable,
                StockStatus = status
            };
        }

        // MAP MEDICINE TO RESPONSE DTO
        private static MedicineResponseDto MapToDto(
            Medicine medicine, string? categoryName)
        {
            return new MedicineResponseDto
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Description = medicine.Description,
                Price = medicine.Price,
                StockQuantity = medicine.StockQuantity,
                ImageUrl = medicine.ImageUrl,
                IsPrescriptionRequired = medicine.IsPrescriptionRequired,
                IsAvailable = medicine.IsAvailable,
                CategoryId = medicine.CategoryId,
                CategoryName = categoryName
            };
        }
    }
}
