using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Cyvil.Mvc.Data.Migrations
{
    public partial class UpdateApp0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timeslots_Interviews_InterviewId",
                table: "Timeslots");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.RenameColumn(
                name: "InterviewId",
                table: "Timeslots",
                newName: "InterviewScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Timeslots_InterviewId",
                table: "Timeslots",
                newName: "IX_Timeslots_InterviewScheduleId");

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Interviews",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "TimeslotId",
                table: "Interviews",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "InterviewSchedules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PositionId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterviewSchedules_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_TimeslotId",
                table: "Interviews",
                column: "TimeslotId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedules_PositionId",
                table: "InterviewSchedules",
                column: "PositionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Timeslots_TimeslotId",
                table: "Interviews",
                column: "TimeslotId",
                principalTable: "Timeslots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Timeslots_InterviewSchedules_InterviewScheduleId",
                table: "Timeslots",
                column: "InterviewScheduleId",
                principalTable: "InterviewSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Timeslots_TimeslotId",
                table: "Interviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Timeslots_InterviewSchedules_InterviewScheduleId",
                table: "Timeslots");

            migrationBuilder.DropTable(
                name: "InterviewSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Interviews_TimeslotId",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "TimeslotId",
                table: "Interviews");

            migrationBuilder.RenameColumn(
                name: "InterviewScheduleId",
                table: "Timeslots",
                newName: "InterviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Timeslots_InterviewScheduleId",
                table: "Timeslots",
                newName: "IX_Timeslots_InterviewId");

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CandidateId = table.Column<string>(type: "varchar(255)", nullable: false),
                    TimeslotId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsCancelled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_AspNetUsers_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Timeslots_TimeslotId",
                        column: x => x.TimeslotId,
                        principalTable: "Timeslots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CandidateId",
                table: "Appointments",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TimeslotId",
                table: "Appointments",
                column: "TimeslotId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Timeslots_Interviews_InterviewId",
                table: "Timeslots",
                column: "InterviewId",
                principalTable: "Interviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
