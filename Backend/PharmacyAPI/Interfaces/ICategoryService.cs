
using PharmacyAPI.DTOs.Category;

namespace PharmacyAPI.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto dto);

        Task<List<CategoryResponseDto>> GetAllCategoriesAsync();

        Task<CategoryResponseDto?> GetCategoryByIdAsync(int id);

        Task<CategoryResponseDto?> UpdateCategoryAsync(int id, CreateCategoryDto dto);

        Task<bool> DeleteCategoryAsync(int id);
    }
}
