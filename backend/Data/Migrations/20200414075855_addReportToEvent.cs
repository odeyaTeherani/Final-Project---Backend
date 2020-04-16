using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Data.Migrations
{
    public partial class addReportToEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Reports",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_EventId",
                table: "Reports",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Events_EventId",
                table: "Reports",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Events_EventId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_EventId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Reports");
        }
    }
}
