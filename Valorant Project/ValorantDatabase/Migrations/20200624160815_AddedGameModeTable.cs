using Microsoft.EntityFrameworkCore.Migrations;

namespace ValorantDatabase.Migrations
{
    public partial class AddedGameModeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModeID",
                table: "GameLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GameModes",
                columns: table => new
                {
                    ModeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModeName = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    ModeDiscription = table.Column<string>(unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeID", x => x.ModeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameLogs_ModeID",
                table: "GameLogs",
                column: "ModeID");

            migrationBuilder.AddForeignKey(
                name: "FK_GameMode",
                table: "GameLogs",
                column: "ModeID",
                principalTable: "GameModes",
                principalColumn: "ModeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameMode",
                table: "GameLogs");

            migrationBuilder.DropTable(
                name: "GameModes");

            migrationBuilder.DropIndex(
                name: "IX_GameLogs_ModeID",
                table: "GameLogs");

            migrationBuilder.DropColumn(
                name: "ModeID",
                table: "GameLogs");
        }
    }
}
