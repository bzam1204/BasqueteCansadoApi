using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasqueteCansadoApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateCompleteSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "OverallIndex",
                table: "Players",
                newName: "ShirtNumber");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Players",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatisticCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerTeamMatch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsStarter = table.Column<bool>(type: "boolean", nullable: false),
                    Team = table.Column<bool>(type: "boolean", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false),
                    MatchId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerTeamMatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerTeamMatch_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerTeamMatch_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statistic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    MatchId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Team = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statistic_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statistic_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statistic_StatisticCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "StatisticCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTeamMatch_MatchId",
                table: "PlayerTeamMatch",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTeamMatch_PlayerId",
                table: "PlayerTeamMatch",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Statistic_CategoryId",
                table: "Statistic",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Statistic_MatchId",
                table: "Statistic",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Statistic_PlayerId",
                table: "Statistic",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerTeamMatch");

            migrationBuilder.DropTable(
                name: "Statistic");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "StatisticCategory");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "ShirtNumber",
                table: "Players",
                newName: "OverallIndex");

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
