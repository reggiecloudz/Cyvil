using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cyvil.Mvc.Data.Migrations
{
    public partial class UpdateMem7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectManagerId",
                table: "Teams",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "ProjectManagerId",
                table: "Positions",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "ProjectManagerId",
                table: "ActionItems",
                type: "longtext",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectManagerId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ProjectManagerId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "ProjectManagerId",
                table: "ActionItems");
        }
    }
}
