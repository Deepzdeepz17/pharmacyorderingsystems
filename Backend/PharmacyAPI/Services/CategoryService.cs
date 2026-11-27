
using PharmacyAPI.DTOs.Category;
using PharmacyAPI.Interfaces;
using PharmacyAPI.Models;

namespace PharmacyAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // Create Category
        public async Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var existingCategories = await _categoryRepository.GetAllAsync();

            bool categoryExists = existingCategories.Any(c =>
                c.Name.Equals(dto.Name.Trim(), StringComparison.OrdinalIgnoreCase));

            if (categoryExists)
            {
                throw new InvalidOperationException("Category already exists.");
            }

            var category = new Category
            {
                Name = dto.Name.Trim(),
                Description = dto.Description?.Trim()
            };

            var savedCategory = await _categoryRepository.AddAsync(category);

            return new CategoryResponseDto
            {
                Id = savedCategory.Id,
                Name = savedCategory.Name,
                Description = savedCategory.Description
            };
        }

        // Get All Categories
        public async Task<List<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories.Select(c => new CategoryResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();
        }

        // Get Category By ID
        public async Task<CategoryResponseDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return null;

            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        // Update Category
        public async Task<CategoryResponseDto?> UpdateCategoryAsync(int id, CreateCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return null;

            var existingCategories = await _categoryRepository.GetAllAsync();

            bool duplicateExists = existingCategories.Any(c =>
                c.Id != id &&
                c.Name.Equals(dto.Name.Trim(), StringComparison.OrdinalIgnoreCase));

            if (duplicateExists)
                throw new InvalidOperationException("Category name already exists.");

            category.Name = dto.Name.Trim();
            category.Description = dto.Description?.Trim();

            await _categoryRepository.UpdateAsync(category);

            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        // Delete Category
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return false;

            await _categoryRepository.DeleteAsync(category);

            return true;
        }
    }
}
