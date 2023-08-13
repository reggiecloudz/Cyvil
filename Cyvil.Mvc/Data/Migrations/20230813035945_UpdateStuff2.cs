using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cyvil.Mvc.Data.Migrations
{
    public partial class UpdateStuff2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Timeslots_TimeslotId",
                table: "Interviews");

            migrationBuilder.AlterColumn<long>(
                name: "TimeslotId",
                table: "Interviews",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Timeslots_TimeslotId",
                table: "Interviews",
                column: "TimeslotId",
                principalTable: "Timeslots",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Timeslots_TimeslotId",
                table: "Interviews");

            migrationBuilder.AlterColumn<long>(
                name: "TimeslotId",
                table: "Interviews",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Timeslots_TimeslotId",
                table: "Interviews",
                column: "TimeslotId",
                principalTable: "Timeslots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
