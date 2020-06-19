using Microsoft.EntityFrameworkCore.Migrations;

namespace ValorantDatabase.Migrations
{
    public partial class AddedMapImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Maps",
                unicode: false,
                maxLength: 35,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LayoutImagePath",
                table: "Maps",
                unicode: false,
                maxLength: 35,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Maps");

            migrationBuilder.DropColumn(
                name: "LayoutImagePath",
                table: "Maps");
        }
    }
}
