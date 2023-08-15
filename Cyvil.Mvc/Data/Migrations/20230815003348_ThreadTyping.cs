using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cyvil.Mvc.Data.Migrations
{
    public partial class ThreadTyping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageThreadType",
                table: "MessageThreads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "MessageThreadTypeId",
                table: "MessageThreads",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageThreadType",
                table: "MessageThreads");

            migrationBuilder.DropColumn(
                name: "MessageThreadTypeId",
                table: "MessageThreads");
        }
    }
}
