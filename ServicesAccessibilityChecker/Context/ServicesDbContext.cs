using Microsoft.EntityFrameworkCore;
using ServicesAccessibilityChecker.Models;

namespace ServicesAccessibilityChecker.Context
{
    public class ServicesDbContext : DbContext
    {
        public ServicesDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Ibonus> Ibonuses { get; set; }
        public DbSet<Refdata> Refdatas { get; set; }
    }
}
