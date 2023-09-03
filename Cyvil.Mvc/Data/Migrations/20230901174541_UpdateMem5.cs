using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cyvil.Mvc.Data.Migrations
{
    public partial class UpdateMem5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Meetings_MeetingId",
                table: "Invitations");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Projects_ProjectId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_ProjectId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_MeetingId",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "MeetingId",
                table: "Invitations");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Invitations");

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "Meetings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MeetingId",
                table: "Invitations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_ProjectId",
                table: "Meetings",
                column: "ProjectId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Projects_ProjectId",
                table: "Meetings",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
