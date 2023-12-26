using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaythosAerospaceMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchoo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalDetails",
                table: "ManufacturingProgresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AirportCity",
                table: "ManufacturingProgresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShippingComplete",
                table: "ManufacturingProgresses",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShippingDate",
                table: "ManufacturingProgresses",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalDetails",
                table: "ManufacturingProgresses");

            migrationBuilder.DropColumn(
                name: "AirportCity",
                table: "ManufacturingProgresses");

            migrationBuilder.DropColumn(
                name: "ShippingComplete",
                table: "ManufacturingProgresses");

            migrationBuilder.DropColumn(
                name: "ShippingDate",
                table: "ManufacturingProgresses");
        }
    }
}
