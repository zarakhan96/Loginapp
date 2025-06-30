using Loginapp.Models;
using Microsoft.EntityFrameworkCore;

namespace Loginapp.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnectionString"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OtpEntry> OtpEntries { get; set; }

    }
}
