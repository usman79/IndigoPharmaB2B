using Microsoft.EntityFrameworkCore.Migrations;

namespace IndigoAdmin.Migrations
{
    public partial class ChangeInUserAccountOtId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedOrderTakerId",
                table: "UserAccount",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedOrderTakerId",
                table: "UserAccount");
        }
    }
}
