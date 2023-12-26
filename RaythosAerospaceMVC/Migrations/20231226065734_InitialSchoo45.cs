using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaythosAerospaceMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchoo45 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    DeliveryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirportCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdditionalDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingComplete = table.Column<bool>(type: "bit", nullable: true),
                    ManufacturingStatusId = table.Column<int>(type: "int", nullable: true),
                    AirframeProgress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvionicsSystemsProgress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnginesProgress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InteriorProgress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverallProgress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufacturingComplete = table.Column<bool>(type: "bit", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeatingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InteriorDesign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EngineType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaximumSpeed = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FuelCapacity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SeatingCapacity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.DeliveryId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Delivery");
        }
    }
}
