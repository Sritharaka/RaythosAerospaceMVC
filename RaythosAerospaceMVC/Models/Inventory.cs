using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaythosAerospaceMVC.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }
        public int? AircraftId { get; set; }
        public string? ModelName { get; set; }
        public string? Manufacturer { get; set; }
        public string? Description { get; set; }
        public string? SeatingType { get; set; }
        public string? InteriorDesign { get; set; }
        public string? AdditionalFeatures { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? BasePrice { get; set; }
        public string? EngineType { get; set; }
        public decimal? MaximumSpeed { get; set; }
        public decimal? FuelCapacity { get; set; }
        public decimal? SeatingCapacity { get; set; }
        public decimal? Weight { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        [NotMapped]
        public IFormFile? uploadedImage { get; set; }

        public decimal? InventorySeats { get; set; }

        public decimal? InventoryEngines { get; set; }

        public decimal? InventoryAircraftBody { get; set; }

        public decimal? InventoryAirframes { get; set; }

        public decimal? InventoryAvionicsSystems { get; set; }

        public decimal? InventoryFuelTanks { get; set; }

        public bool? ManufacturingComplete { get; set; }

        public string? InventoryDescription { get; set; }






    }
}
