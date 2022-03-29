using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AplicatieFotbal.DataAccess.Migrations
{
    public partial class updateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchCard");

            migrationBuilder.DropTable(
                name: "MatchChange");

            migrationBuilder.AlterColumn<int>(
                name: "Minute",
                table: "MatchGoal",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Match",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "Match",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Team1Corners",
                table: "Match",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Team1Fouls",
                table: "Match",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Team1GKSaves",
                table: "Match",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Team1Offside",
                table: "Match",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Team1RedCards",
                table: "Match",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Team1YellowCards",
                table: "Match",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Team2Corners",
                table: "Match",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Team2Fouls",
                table: "Match",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Team2GKSaves",
                table: "Match",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Team2Offside",
                table: "Match",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Team2RedCards",
                table: "Match",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Team2YellowCards",
                table: "Match",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Match_LeagueId",
                table: "Match",
                column: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_League_LeagueId",
                table: "Match",
                column: "LeagueId",
                principalTable: "League",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_League_LeagueId",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_LeagueId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Team1Corners",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Team1Fouls",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Team1GKSaves",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Team1Offside",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Team1RedCards",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Team1YellowCards",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Team2Corners",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Team2Fouls",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Team2GKSaves",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Team2Offside",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Team2RedCards",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Team2YellowCards",
                table: "Match");

            migrationBuilder.AlterColumn<string>(
                name: "Minute",
                table: "MatchGoal",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "MatchCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsRed = table.Column<bool>(type: "bit", nullable: false),
                    IsYellow = table.Column<bool>(type: "bit", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchCard_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchCard_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchChange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerId1 = table.Column<int>(type: "int", nullable: false),
                    PlayerId2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchChange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchChange_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchCard_MatchId",
                table: "MatchCard",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchCard_PlayerId",
                table: "MatchCard",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchChange_MatchId",
                table: "MatchChange",
                column: "MatchId");
        }
    }
}
