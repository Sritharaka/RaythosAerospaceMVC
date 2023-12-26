using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaythosAerospaceMVC.Models
{
    public class ManufacturingProgress
    {
        [Key]
        public int ManufacturingStatusId { get; set; }
        public string? Status { get; set; }
        public string? AirframeProgress { get; set; }
        public string? AvionicsSystemsProgress { get; set; }
        public string? EnginesProgress { get; set; }
        public string? InteriorProgress { get; set; }
        public string? OverallProgress { get; set; }
        public bool? ManufacturingComplete { get; set; }
        public string? Description { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public string? ModelName { get; set; }
        public string? Manufacturer { get; set; }
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

        public int? UserId { get; set; }
        public string? CardNumber { get; set; }
        public string? ExpiryDate { get; set; }
        public string? CVV { get; set; }
        public string? CardHolderName { get; set; }

        public int? Telephone { get; set; }
        public string? EmailAddress { get; set; }

        public int? AircraftId { get; set; }

        public int? PaymentId { get; set; }

        public string? AirportCity { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string? AdditionalDetails { get; set; }
        public bool? ShippingComplete { get; set; }

        public string? DeliveryComplete { get; set; }





    }
}
