using Microsoft.EntityFrameworkCore.Migrations;

namespace ValorantDatabase.Migrations
{
    public partial class addedRankTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RankID",
                table: "GameLogs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ranks",
                columns: table => new
                {
                    RankID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RankName = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    ImagePath = table.Column<string>(unicode: false, maxLength: 35, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankID", x => x.RankID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameLogs_RankID",
                table: "GameLogs",
                column: "RankID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rank",
                table: "GameLogs",
                column: "RankID",
                principalTable: "Ranks",
                principalColumn: "RankID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rank",
                table: "GameLogs");

            migrationBuilder.DropTable(
                name: "Ranks");

            migrationBuilder.DropIndex(
                name: "IX_GameLogs_RankID",
                table: "GameLogs");

            migrationBuilder.DropColumn(
                name: "RankID",
                table: "GameLogs");
        }
    }
}
