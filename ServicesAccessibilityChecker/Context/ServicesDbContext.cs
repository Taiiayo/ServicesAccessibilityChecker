using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace ServicesAccessibilityChecker.Context
{
    public class ServicesDbContext : DbContext
    {
        private JToken Config => JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json")));
        private string ConnectionString => Config["ConnectionStrings"]["DefaultConnection"].ToString();
        public ServicesDbContext()
        {
        }

        public ServicesDbContext(DbContextOptions<ServicesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Ibonus> Ibonuses { get; set; }
        public DbSet<Refdata> Refdatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(ConnectionString);
            }
        }
    }
}
