using RaythosAerospaceMVC.Models;

namespace RaythosAerospaceMVC.Repository
{
    public class ReportViewModel
    {
        public IEnumerable<Payment> Payments { get; set; }
        public IEnumerable<Inventory> Inventory { get; set; }
        public IEnumerable<Aircraft> Aircrafts { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<ManufacturingProgress> ManufacturingProgresses { get; set; }
    }
}
