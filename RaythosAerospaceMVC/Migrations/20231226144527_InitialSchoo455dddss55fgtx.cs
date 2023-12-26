using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaythosAerospaceMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchoo455dddss55fgtx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryCompleteDate",
                table: "Delivery",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryCompleteDescription",
                table: "Delivery",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarrentyYears",
                table: "Delivery",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryCompleteDate",
                table: "Delivery");

            migrationBuilder.DropColumn(
                name: "DeliveryCompleteDescription",
                table: "Delivery");

            migrationBuilder.DropColumn(
                name: "WarrentyYears",
                table: "Delivery");
        }
    }
}
