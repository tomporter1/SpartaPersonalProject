using Microsoft.EntityFrameworkCore.Migrations;

namespace ValorantDatabase.Migrations
{
    public partial class AddedRankAdjustments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RankAdjustmentID",
                table: "GameLogs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RankAdjustments",
                columns: table => new
                {
                    AdjustmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdjustmentName = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    ImagePath = table.Column<string>(unicode: false, maxLength: 35, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdjustmentID", x => x.AdjustmentID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameLogs_RankAdjustmentID",
                table: "GameLogs",
                column: "RankAdjustmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_RankAdjustment",
                table: "GameLogs",
                column: "RankAdjustmentID",
                principalTable: "RankAdjustments",
                principalColumn: "AdjustmentID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RankAdjustment",
                table: "GameLogs");

            migrationBuilder.DropTable(
                name: "RankAdjustments");

            migrationBuilder.DropIndex(
                name: "IX_GameLogs_RankAdjustmentID",
                table: "GameLogs");

            migrationBuilder.DropColumn(
                name: "RankAdjustmentID",
                table: "GameLogs");
        }
    }
}
