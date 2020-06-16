using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class SeedSubRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SubRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Police" });

            migrationBuilder.InsertData(
                table: "SubRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Fire Fighter" });

            migrationBuilder.InsertData(
                table: "SubRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Paramedic" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubRoles",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
