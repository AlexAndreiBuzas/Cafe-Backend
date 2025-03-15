using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddPickupCodeToOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PickupCode",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickupCode",
                table: "Orders");
        }
    }
}
