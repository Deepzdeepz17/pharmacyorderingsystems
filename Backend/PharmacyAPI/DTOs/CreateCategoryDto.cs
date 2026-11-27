using System.ComponentModel.DataAnnotations;

namespace PharmacyAPI.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}