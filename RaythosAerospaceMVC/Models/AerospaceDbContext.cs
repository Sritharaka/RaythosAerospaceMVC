using Microsoft.EntityFrameworkCore;

namespace RaythosAerospaceMVC.Models
{
    public class AerospaceDbContext : DbContext
    {
        public AerospaceDbContext(DbContextOptions<AerospaceDbContext> options): base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
    }
}
