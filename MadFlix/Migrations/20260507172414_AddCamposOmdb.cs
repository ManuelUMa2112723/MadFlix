using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadFlix.Migrations
{
    /// <inheritdoc />
    public partial class AddCamposOmdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImdbId",
                table: "Filmes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImdbRating",
                table: "Filmes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "Filmes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rated",
                table: "Filmes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Runtime",
                table: "Filmes",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImdbId",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "ImdbRating",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "Rated",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "Runtime",
                table: "Filmes");
        }
    }
}
