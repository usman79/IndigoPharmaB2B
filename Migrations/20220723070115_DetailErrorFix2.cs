using Microsoft.EntityFrameworkCore.Migrations;

namespace IndigoAdmin.Migrations
{
    public partial class DetailErrorFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdditionalDiscount",
                table: "OrderDetail",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalDiscount",
                table: "OrderDetail");
        }
    }
}
