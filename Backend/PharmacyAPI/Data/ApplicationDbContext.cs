
using Microsoft.EntityFrameworkCore;
using PharmacyAPI.Models;

namespace PharmacyAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
            public DbSet<Category> Categories { get; set; }
            public DbSet<User> Users { get; set; }

            public DbSet<Role> Roles { get; set; }

    }
    }