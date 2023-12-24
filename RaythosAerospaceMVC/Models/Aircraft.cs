using System.ComponentModel.DataAnnotations.Schema;

namespace RaythosAerospaceMVC.Models
{
    public class Aircraft
    {
        public int Id { get; set; }
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


    }
}
