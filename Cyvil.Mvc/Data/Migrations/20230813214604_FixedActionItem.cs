using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cyvil.Mvc.Data.Migrations
{
    public partial class FixedActionItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "ActionItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ActionItems");
        }
    }
}
