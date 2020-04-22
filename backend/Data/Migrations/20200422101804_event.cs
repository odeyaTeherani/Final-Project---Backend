using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Data.Migrations
{
    public partial class @event : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventType",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "NumOfEv",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "SeverityLevel",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "EventType",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "SeverityLevel",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Reports",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Casualties",
                table: "Reports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EventTypeId",
                table: "Reports",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Reports",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeverityLevelType",
                table: "Reports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EventTypeId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeverityLevelType",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    ImageSize = table.Column<string>(nullable: true),
                    EventId = table.Column<int>(nullable: true),
                    ReportId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Image_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_EventTypeId",
                table: "Reports",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_LocationId",
                table: "Reports",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId",
                table: "Events",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationId",
                table: "Events",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_EventId",
                table: "Image",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ReportId",
                table: "Image",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventTypes_EventTypeId",
                table: "Events",
                column: "EventTypeId",
                principalTable: "EventTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_EventTypes_EventTypeId",
                table: "Reports",
                column: "EventTypeId",
                principalTable: "EventTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Locations_LocationId",
                table: "Reports",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventTypes_EventTypeId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_EventTypes_EventTypeId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Locations_LocationId",
                table: "Reports");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Reports_EventTypeId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_LocationId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventTypeId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_LocationId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Casualties",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "EventTypeId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "SeverityLevelType",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "EventTypeId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "SeverityLevelType",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventType",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumOfEv",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SeverityLevel",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventType",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeverityLevel",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
