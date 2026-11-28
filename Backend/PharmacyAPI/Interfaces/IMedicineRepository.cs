using PharmacyAPI.Models;

namespace PharmacyAPI.Interfaces
{
    public interface IMedicineRepository
    {
        Task<Medicine> AddAsync(Medicine medicine);
        Task<List<Medicine>> GetAllAsync();
        Task<Medicine?> GetByIdAsync(int id);
        Task UpdateAsync(Medicine medicine);
        Task DeleteAsync(Medicine medicine);
    }
}