using Microsoft.EntityFrameworkCore;

namespace ServicesAccessibilityChecker.Context
{
    public class ServicesDbContext : DbContext
    {
        //public ServicesDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Ibonus> Ibonuses { get; set; }
        public DbSet<Refdata> Refdatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=blogging.db");
        }
    }
}
