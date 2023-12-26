using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaythosAerospaceMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchoo455 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Telephone",
                table: "Delivery",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Delivery");
        }
    }
}
