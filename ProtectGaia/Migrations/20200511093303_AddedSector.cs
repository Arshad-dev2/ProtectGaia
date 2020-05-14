using Microsoft.EntityFrameworkCore.Migrations;

namespace ProtectGaia.Migrations
{
    public partial class AddedSector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sector",
                table: "ChallengeData",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sector",
                table: "ChallengeData");
        }
    }
}
