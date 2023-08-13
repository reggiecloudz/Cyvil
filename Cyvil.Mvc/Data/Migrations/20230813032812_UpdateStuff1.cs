using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cyvil.Mvc.Data.Migrations
{
    public partial class UpdateStuff1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "InitialMessage",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "InterviewDate",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Interviews");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "Interviews",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "InitialMessage",
                table: "Interviews",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "InterviewDate",
                table: "Interviews",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "Interviews",
                type: "longtext",
                nullable: false);
        }
    }
}
