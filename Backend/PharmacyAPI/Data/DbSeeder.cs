
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmacyAPI.Models;

namespace PharmacyAPI.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAdminAsync(
            ApplicationDbContext context,
            IConfiguration configuration,
            IPasswordHasher<User> passwordHasher)
        {
            var adminRole = await context.Roles
                .FirstOrDefaultAsync(r => r.Name == "Admin");

            if (adminRole == null)
            {
                adminRole = new Role
                {
                    Name = "Admin"
                };

                context.Roles.Add(adminRole);
                await context.SaveChangesAsync();
            }

            var email = configuration["AdminSeed:Email"]
                ?? throw new InvalidOperationException("Admin email is missing.");

            var name = configuration["AdminSeed:Name"]
                ?? throw new InvalidOperationException("Admin name is missing.");

            var password = configuration["AdminSeed:Password"]
                ?? throw new InvalidOperationException("Admin password is missing.");

            var existingAdmin = await context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (existingAdmin == null)
            {
                var admin = new User
                {
                    Name = name,
                    Email = email,
                    RoleId = adminRole.Id
                };

                admin.PasswordHash = passwordHasher.HashPassword(
                    admin, password);

                context.Users.Add(admin);
                await context.SaveChangesAsync();
            }
        }
    }
}
