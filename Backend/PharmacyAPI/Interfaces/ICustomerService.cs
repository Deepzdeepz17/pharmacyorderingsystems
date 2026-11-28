
using PharmacyAPI.DTOs.Customer;

namespace PharmacyAPI.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerResponseDto>> GetAllCustomersAsync();

        Task<CustomerResponseDto?> GetCustomerByIdAsync(int id);

        Task<List<CustomerResponseDto>> SearchCustomersAsync(
            string searchTerm);

        Task<CustomerResponseDto?> UpdateCustomerStatusAsync(
            int id, CustomerStatusDto dto);
    }
}
