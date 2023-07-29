using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cyvil.Mvc.Data.Migrations
{
    public partial class UpdatedApplicant2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Applicants");

            migrationBuilder.RenameColumn(
                name: "StatusCode",
                table: "Applicants",
                newName: "ApplicantStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApplicantStatus",
                table: "Applicants",
                newName: "StatusCode");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Applicants",
                type: "longtext",
                nullable: false);
        }
    }
}
