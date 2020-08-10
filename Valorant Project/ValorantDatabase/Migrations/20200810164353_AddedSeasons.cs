using Microsoft.EntityFrameworkCore.Migrations;

namespace ValorantDatabase.Migrations
{
    public partial class AddedSeasons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Season",
                table: "GameLogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Season",
                table: "GameLogs");
        }
    }
}
