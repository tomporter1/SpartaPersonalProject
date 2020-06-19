using Microsoft.EntityFrameworkCore.Migrations;

namespace ValorantDatabase.Migrations
{
    public partial class AddedClassIcons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "AgentType",
                unicode: false,
                maxLength: 35,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "AgentType");
        }
    }
}
