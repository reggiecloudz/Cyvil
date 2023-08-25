using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cyvil.Mvc.Data.Migrations
{
    public partial class Update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Projects",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "AspNetUsers",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Projects_Slug",
                table: "Projects",
                column: "Slug");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_Slug",
                table: "AspNetUsers",
                column: "Slug");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Projects_Slug",
                table: "Projects");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_Slug",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Projects",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");
        }
    }
}
