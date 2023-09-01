using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cyvil.Mvc.Data.Migrations
{
    public partial class UpdateMem3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Invitations");

            migrationBuilder.AddColumn<long>(
                name: "MeetingId",
                table: "Invitations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_MeetingId",
                table: "Invitations",
                column: "MeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Meetings_MeetingId",
                table: "Invitations",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Meetings_MeetingId",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_MeetingId",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "MeetingId",
                table: "Invitations");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Meetings",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Meetings",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Invitations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "TypeId",
                table: "Invitations",
                type: "bigint",
                nullable: true);
        }
    }
}
