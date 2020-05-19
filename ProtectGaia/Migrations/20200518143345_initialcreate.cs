using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProtectGaia.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChallengeData",
                columns: table => new
                {
                    ChallengeId = table.Column<int>(nullable: false),
                    LevelId = table.Column<int>(nullable: false),
                    ChallengeTitle = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true),
                    CarbonScore = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserEmail = table.Column<string>(maxLength: 50, nullable: false),
                    LevelId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    UserImageUrl = table.Column<string>(nullable: true),
                    TotalTaskCompleted = table.Column<int>(nullable: false),
                    LevelCompletedTask = table.Column<int>(nullable: false),
                    PendingTask = table.Column<int>(nullable: false),
                    TotalPointScored = table.Column<int>(nullable: false),
                    CarbonScore = table.Column<int>(nullable: false),
                    Activity = table.Column<string>(nullable: true),
                    CarbonActivity = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false),
                    IsTask1Completed = table.Column<bool>(nullable: false),
                    IsTask2Completed = table.Column<bool>(nullable: false),
                    IsTask3Completed = table.Column<bool>(nullable: false),
                    IsTask4Completed = table.Column<bool>(nullable: false),
                    IsTask5Completed = table.Column<bool>(nullable: false),
                    IsFirstTimeLogin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => new { x.UserEmail, x.LevelId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallengeData");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
