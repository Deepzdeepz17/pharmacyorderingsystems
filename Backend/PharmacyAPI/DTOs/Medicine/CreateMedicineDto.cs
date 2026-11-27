
using System.ComponentModel.DataAnnotations;

namespace PharmacyAPI.DTOs.Medicine
{
    public class CreateMedicineDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Range(0.01, 999999)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsPrescriptionRequired { get; set; }

        public bool IsAvailable { get; set; } = true;

        [Required]
        public int CategoryId { get; set; }
    }
}
