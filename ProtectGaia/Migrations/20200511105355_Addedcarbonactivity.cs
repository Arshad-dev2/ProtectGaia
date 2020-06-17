using Microsoft.EntityFrameworkCore.Migrations;

namespace ProtectGaia.Migrations
{
    public partial class Addedcarbonactivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarbonActivity",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarbonActivity",
                table: "User");
        }
    }
}
