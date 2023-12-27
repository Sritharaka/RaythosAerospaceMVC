using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaythosAerospaceMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchdscsd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InventoryDescription",
                table: "Inventory",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventoryDescription",
                table: "Inventory");
        }
    }
}
