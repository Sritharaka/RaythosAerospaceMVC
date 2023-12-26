using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaythosAerospaceMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialSc456 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OverallProcess",
                table: "ManufacturingProgresses",
                newName: "OverallProgress");

            migrationBuilder.RenameColumn(
                name: "ManufacturingProcess",
                table: "ManufacturingProgresses",
                newName: "ManufacturingComplete");

            migrationBuilder.RenameColumn(
                name: "InteriorProcess",
                table: "ManufacturingProgresses",
                newName: "InteriorProgress");

            migrationBuilder.RenameColumn(
                name: "EnginesProcess",
                table: "ManufacturingProgresses",
                newName: "EnginesProgress");

            migrationBuilder.RenameColumn(
                name: "AvionicsSystemsProcess",
                table: "ManufacturingProgresses",
                newName: "AvionicsSystemsProgress");

            migrationBuilder.RenameColumn(
                name: "AirframeProcess",
                table: "ManufacturingProgresses",
                newName: "AirframeProgress");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "ManufacturingProgresses",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "ManufacturingProgresses");

            migrationBuilder.RenameColumn(
                name: "OverallProgress",
                table: "ManufacturingProgresses",
                newName: "OverallProcess");

            migrationBuilder.RenameColumn(
                name: "ManufacturingComplete",
                table: "ManufacturingProgresses",
                newName: "ManufacturingProcess");

            migrationBuilder.RenameColumn(
                name: "InteriorProgress",
                table: "ManufacturingProgresses",
                newName: "InteriorProcess");

            migrationBuilder.RenameColumn(
                name: "EnginesProgress",
                table: "ManufacturingProgresses",
                newName: "EnginesProcess");

            migrationBuilder.RenameColumn(
                name: "AvionicsSystemsProgress",
                table: "ManufacturingProgresses",
                newName: "AvionicsSystemsProcess");

            migrationBuilder.RenameColumn(
                name: "AirframeProgress",
                table: "ManufacturingProgresses",
                newName: "AirframeProcess");
        }
    }
}
