using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaythosAerospaceMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialSc45 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AirframeProcess",
                table: "ManufacturingProgresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvionicsSystemsProcess",
                table: "ManufacturingProgresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnginesProcess",
                table: "ManufacturingProgresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InteriorProcess",
                table: "ManufacturingProgresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OverallProcess",
                table: "ManufacturingProgresses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirframeProcess",
                table: "ManufacturingProgresses");

            migrationBuilder.DropColumn(
                name: "AvionicsSystemsProcess",
                table: "ManufacturingProgresses");

            migrationBuilder.DropColumn(
                name: "EnginesProcess",
                table: "ManufacturingProgresses");

            migrationBuilder.DropColumn(
                name: "InteriorProcess",
                table: "ManufacturingProgresses");

            migrationBuilder.DropColumn(
                name: "OverallProcess",
                table: "ManufacturingProgresses");
        }
    }
}
