using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cyvil.Mvc.Data.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "AspNetUsers");
        }
    }
}
