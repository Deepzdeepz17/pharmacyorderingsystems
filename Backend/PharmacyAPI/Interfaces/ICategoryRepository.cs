using PharmacyAPI.Models;

namespace PharmacyAPI.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> AddAsync(Category category);

        Task<List<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(int id);

        Task UpdateAsync(Category category);

        Task DeleteAsync(Category category);
    }
}