
using PharmacyAPI.DTOs.Medicine;
using PharmacyAPI.DTOs.Stock;


namespace PharmacyAPI.Interfaces
{
    public interface IMedicineService
    {
        Task<MedicineResponseDto> CreateMedicineAsync(CreateMedicineDto dto);

        Task<List<MedicineResponseDto>> GetAllMedicinesAsync();

        Task<MedicineResponseDto?> GetMedicineByIdAsync(int id);

        Task<MedicineResponseDto?> UpdateMedicineAsync(int id, CreateMedicineDto dto);

        Task<bool> DeleteMedicineAsync(int id);
        Task<StockResponseDto> UpdateStockAsync(int medicineId, UpdateStockDto dto);

        Task<List<StockResponseDto>> GetStockDetailsAsync();
    }
}
