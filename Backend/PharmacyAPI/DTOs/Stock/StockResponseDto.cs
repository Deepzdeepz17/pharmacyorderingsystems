
namespace PharmacyAPI.DTOs.Stock
{
    public class StockResponseDto
    {
        public int MedicineId { get; set; }

        public string MedicineName { get; set; } = string.Empty;

        public int StockQuantity { get; set; }

        public bool IsAvailable { get; set; }

        public string StockStatus { get; set; } = string.Empty;
    }
}
