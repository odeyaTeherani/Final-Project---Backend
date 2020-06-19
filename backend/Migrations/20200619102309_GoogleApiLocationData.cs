using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class GoogleApiLocationData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormattedAddress",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GoogleLatitude",
                table: "Locations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GoogleLongitude",
                table: "Locations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "GooglePlacesDbId",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceId",
                table: "Locations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormattedAddress",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "GoogleLatitude",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "GoogleLongitude",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "GooglePlacesDbId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Locations");
        }
    }
}
