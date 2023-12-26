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
        public DbSet<Otps> Otps { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ManufacturingProgress> ManufacturingProgresses { get; set; }
        public DbSet<Delivery> Delivery { get; set; }
        public DbSet<Contact> Contact { get; set; }




    }
}
