
using System.ComponentModel.DataAnnotations;

namespace PharmacyAPI.DTOs.Stock
{
    public class UpdateStockDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int StockQuantity { get; set; }
    }
}
