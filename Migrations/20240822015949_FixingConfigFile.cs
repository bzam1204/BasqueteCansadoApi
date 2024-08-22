using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasqueteCansadoApi.Migrations
{
    /// <inheritdoc />
    public partial class FixingConfigFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTeamMatch_Match_MatchId",
                table: "PlayerTeamMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTeamMatch_Players_PlayerId",
                table: "PlayerTeamMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistic_Match_MatchId",
                table: "Statistic");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistic_Players_PlayerId",
                table: "Statistic");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistic_StatisticCategory_CategoryId",
                table: "Statistic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatisticCategory",
                table: "StatisticCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statistic",
                table: "Statistic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerTeamMatch",
                table: "PlayerTeamMatch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Match",
                table: "Match");

            migrationBuilder.RenameTable(
                name: "StatisticCategory",
                newName: "StatisticCategories");

            migrationBuilder.RenameTable(
                name: "Statistic",
                newName: "Statistics");

            migrationBuilder.RenameTable(
                name: "PlayerTeamMatch",
                newName: "PlayerTeamMatches");

            migrationBuilder.RenameTable(
                name: "Match",
                newName: "Matches");

            migrationBuilder.RenameIndex(
                name: "IX_Statistic_PlayerId",
                table: "Statistics",
                newName: "IX_Statistics_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Statistic_MatchId",
                table: "Statistics",
                newName: "IX_Statistics_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_Statistic_CategoryId",
                table: "Statistics",
                newName: "IX_Statistics_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerTeamMatch_PlayerId",
                table: "PlayerTeamMatches",
                newName: "IX_PlayerTeamMatches_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerTeamMatch_MatchId",
                table: "PlayerTeamMatches",
                newName: "IX_PlayerTeamMatches_MatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatisticCategories",
                table: "StatisticCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statistics",
                table: "Statistics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerTeamMatches",
                table: "PlayerTeamMatches",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTeamMatches_Matches_MatchId",
                table: "PlayerTeamMatches",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTeamMatches_Players_PlayerId",
                table: "PlayerTeamMatches",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Matches_MatchId",
                table: "Statistics",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Players_PlayerId",
                table: "Statistics",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_StatisticCategories_CategoryId",
                table: "Statistics",
                column: "CategoryId",
                principalTable: "StatisticCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTeamMatches_Matches_MatchId",
                table: "PlayerTeamMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTeamMatches_Players_PlayerId",
                table: "PlayerTeamMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Matches_MatchId",
                table: "Statistics");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Players_PlayerId",
                table: "Statistics");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_StatisticCategories_CategoryId",
                table: "Statistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statistics",
                table: "Statistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatisticCategories",
                table: "StatisticCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerTeamMatches",
                table: "PlayerTeamMatches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.RenameTable(
                name: "Statistics",
                newName: "Statistic");

            migrationBuilder.RenameTable(
                name: "StatisticCategories",
                newName: "StatisticCategory");

            migrationBuilder.RenameTable(
                name: "PlayerTeamMatches",
                newName: "PlayerTeamMatch");

            migrationBuilder.RenameTable(
                name: "Matches",
                newName: "Match");

            migrationBuilder.RenameIndex(
                name: "IX_Statistics_PlayerId",
                table: "Statistic",
                newName: "IX_Statistic_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Statistics_MatchId",
                table: "Statistic",
                newName: "IX_Statistic_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_Statistics_CategoryId",
                table: "Statistic",
                newName: "IX_Statistic_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerTeamMatches_PlayerId",
                table: "PlayerTeamMatch",
                newName: "IX_PlayerTeamMatch_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerTeamMatches_MatchId",
                table: "PlayerTeamMatch",
                newName: "IX_PlayerTeamMatch_MatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statistic",
                table: "Statistic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatisticCategory",
                table: "StatisticCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerTeamMatch",
                table: "PlayerTeamMatch",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Match",
                table: "Match",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTeamMatch_Match_MatchId",
                table: "PlayerTeamMatch",
                column: "MatchId",
                principalTable: "Match",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTeamMatch_Players_PlayerId",
                table: "PlayerTeamMatch",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistic_Match_MatchId",
                table: "Statistic",
                column: "MatchId",
                principalTable: "Match",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistic_Players_PlayerId",
                table: "Statistic",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistic_StatisticCategory_CategoryId",
                table: "Statistic",
                column: "CategoryId",
                principalTable: "StatisticCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
