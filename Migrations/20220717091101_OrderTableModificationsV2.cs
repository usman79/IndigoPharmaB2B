using Microsoft.EntityFrameworkCore.Migrations;

namespace IndigoAdmin.Migrations
{
    public partial class OrderTableModificationsV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "OrderType",
                table: "Order",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderType",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Order");
        }
    }
}
