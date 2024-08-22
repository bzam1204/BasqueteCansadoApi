using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasqueteCansadoApi.Migrations
{
    /// <inheritdoc />
    public partial class playerCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Done",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Players",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OverallIndex",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "OverallIndex",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Players",
                newName: "Title");

            migrationBuilder.AddColumn<bool>(
                name: "Done",
                table: "Players",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
