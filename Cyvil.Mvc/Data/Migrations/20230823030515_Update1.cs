using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cyvil.Mvc.Data.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionItems_ActionItems_ParentId",
                table: "ActionItems");

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                table: "ActionItems",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionItems_ActionItems_ParentId",
                table: "ActionItems",
                column: "ParentId",
                principalTable: "ActionItems",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionItems_ActionItems_ParentId",
                table: "ActionItems");

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                table: "ActionItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ActionItems_ActionItems_ParentId",
                table: "ActionItems",
                column: "ParentId",
                principalTable: "ActionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
