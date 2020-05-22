using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class deleteName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Reports");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
