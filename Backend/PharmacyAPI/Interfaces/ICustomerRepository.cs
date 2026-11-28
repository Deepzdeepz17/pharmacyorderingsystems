
using PharmacyAPI.Models;

namespace PharmacyAPI.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<User>> GetAllCustomersAsync();

        Task<User?> GetCustomerByIdAsync(int id);

        Task<List<User>> SearchCustomersAsync(string searchTerm);

        Task UpdateAsync(User customer);
    }
}
