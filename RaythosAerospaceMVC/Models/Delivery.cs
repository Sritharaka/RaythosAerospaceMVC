using System.ComponentModel.DataAnnotations;

namespace RaythosAerospaceMVC.Models
{
    public class Delivery
    {
        [Key]
        public int DeliveryId { get; set; }
        public string? AirportCity { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string? AdditionalDetails { get; set; }
        public bool? ShippingComplete { get; set; }
        public int? ManufacturingStatusId { get; set; }

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
        public int? Telephone { get; set; }
        public string? EmailAddress { get; set; }
        public string? CardHolderName { get; set; }
        public string? DeliveryComplete { get; set; }
        public string? WarrentyYears { get; set; }
        public DateTime? DeliveryCompleteDate { get; set; }
        public string? DeliveryCompleteDescription { get; set; }
        public int? AircraftId { get; set; }
        



    }
}
