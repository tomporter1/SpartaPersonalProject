using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ValorantDatabase.Migrations
{
    public partial class ChangedCharLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgentType",
                columns: table => new
                {
                    TypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeID", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    MapID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MapName = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapID", x => x.MapID);
                });

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    AgentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentName = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    AgentTypeID = table.Column<int>(nullable: false),
                    SignatureAbilityName = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    SignatureAbilityDiscription = table.Column<string>(unicode: false, maxLength: 350, nullable: true),
                    UltamateAbilityName = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    UltamateAbilityDiscription = table.Column<string>(unicode: false, maxLength: 350, nullable: true),
                    AbilityOneName = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    AbilityOneDiscription = table.Column<string>(unicode: false, maxLength: 350, nullable: true),
                    AbilityTwoName = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    AbilityTwoDiscription = table.Column<string>(unicode: false, maxLength: 350, nullable: true),
                    Bio = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentID", x => x.AgentID);
                    table.ForeignKey(
                        name: "FK_AgentType",
                        column: x => x.AgentTypeID,
                        principalTable: "AgentType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameLogs",
                columns: table => new
                {
                    GameID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MapID = table.Column<int>(nullable: false),
                    AgentID = table.Column<int>(nullable: false),
                    TeamScore = table.Column<int>(nullable: false),
                    OpponentScore = table.Column<int>(nullable: false),
                    Kills = table.Column<int>(nullable: true),
                    Deaths = table.Column<int>(nullable: true),
                    Assits = table.Column<int>(nullable: true),
                    ADR = table.Column<int>(nullable: true),
                    DateLogged = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameLogs", x => x.GameID);
                    table.ForeignKey(
                        name: "FK_Agent",
                        column: x => x.AgentID,
                        principalTable: "Agents",
                        principalColumn: "AgentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Map",
                        column: x => x.MapID,
                        principalTable: "Maps",
                        principalColumn: "MapID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_AgentTypeID",
                table: "Agents",
                column: "AgentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_GameLogs_AgentID",
                table: "GameLogs",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_GameLogs_MapID",
                table: "GameLogs",
                column: "MapID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameLogs");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "Maps");

            migrationBuilder.DropTable(
                name: "AgentType");
        }
    }
}
