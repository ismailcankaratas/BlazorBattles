using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorBattles.Server.Migrations
{
    public partial class UserBattlesStatics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Battels",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Deffeats",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Victories",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Battels",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Deffeats",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Victories",
                table: "Users");
        }
    }
}
