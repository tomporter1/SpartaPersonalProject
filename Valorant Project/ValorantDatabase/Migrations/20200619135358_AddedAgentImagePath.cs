using Microsoft.EntityFrameworkCore.Migrations;

namespace ValorantDatabase.Migrations
{
    public partial class AddedAgentImagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Agents",
                unicode: false,
                maxLength: 35,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Agents");
        }
    }
}
