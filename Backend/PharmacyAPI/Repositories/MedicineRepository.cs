using Microsoft.EntityFrameworkCore;
using PharmacyAPI.Data;
using PharmacyAPI.Interfaces;
using PharmacyAPI.Models;

namespace PharmacyAPI.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Medicine> AddAsync(Medicine medicine)
        {
            await _context.Medicines.AddAsync(medicine);
            await _context.SaveChangesAsync();
            return medicine;
        }

        public async Task<List<Medicine>> GetAllAsync()
        {
            return await _context.Medicines
                .Include(m => m.Category)
                .ToListAsync();
        }

        public async Task<Medicine?> GetByIdAsync(int id)
        {
            return await _context.Medicines
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAsync(Medicine medicine)
        {
            _context.Medicines.Update(medicine);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Medicine medicine)
        {
            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();
        }
    }
}