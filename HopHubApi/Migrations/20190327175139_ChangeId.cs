using Microsoft.EntityFrameworkCore.Migrations;

namespace HopHubApi.Migrations
{
    public partial class ChangeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReviewId",
                table: "Reviews",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BeerId",
                table: "Beers",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reviews",
                newName: "ReviewId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Beers",
                newName: "BeerId");
        }
    }
}
