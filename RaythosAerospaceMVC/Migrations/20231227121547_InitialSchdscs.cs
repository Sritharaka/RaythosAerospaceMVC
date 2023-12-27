using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaythosAerospaceMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchdscs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AircraftId = table.Column<int>(type: "int", nullable: true),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InventorySeats = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InventoryEngines = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InventoryAircraftBody = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InventoryAirframes = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InventoryAvionicsSystems = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InventoryFuelTanks = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ManufacturingComplete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.InventoryId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventory");
        }
    }
}
