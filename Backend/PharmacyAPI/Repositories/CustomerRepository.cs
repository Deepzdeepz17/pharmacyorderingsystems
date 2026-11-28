
using Microsoft.EntityFrameworkCore;
using PharmacyAPI.Data;
using PharmacyAPI.Interfaces;
using PharmacyAPI.Models;

namespace PharmacyAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET ALL CUSTOMERS
        public async Task<List<User>> GetAllCustomersAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Role != null &&
                            u.Role.Name == "Customer")
                .ToListAsync();
        }

        // GET CUSTOMER BY ID
        public async Task<User?> GetCustomerByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u =>
                    u.Id == id &&
                    u.Role != null &&
                    u.Role.Name == "Customer");
        }

        // SEARCH CUSTOMERS
        public async Task<List<User>> SearchCustomersAsync(
            string searchTerm)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Where(u =>
                    u.Role != null &&
                    u.Role.Name == "Customer" &&
                    (u.Name.Contains(searchTerm) ||
                     u.Email.Contains(searchTerm)))
                .ToListAsync();
        }

        // UPDATE CUSTOMER
        public async Task UpdateAsync(User customer)
        {
            _context.Users.Update(customer);
            await _context.SaveChangesAsync();
        }
    }
}
