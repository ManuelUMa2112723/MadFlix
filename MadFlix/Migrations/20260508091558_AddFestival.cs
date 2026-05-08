using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadFlix.Migrations
{
    /// <inheritdoc />
    public partial class AddFestival : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FestivalId",
                table: "Filmes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Festivais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Genero = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataFim = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Festivais", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filmes_FestivalId",
                table: "Filmes",
                column: "FestivalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Filmes_Festivais_FestivalId",
                table: "Filmes",
                column: "FestivalId",
                principalTable: "Festivais",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filmes_Festivais_FestivalId",
                table: "Filmes");

            migrationBuilder.DropTable(
                name: "Festivais");

            migrationBuilder.DropIndex(
                name: "IX_Filmes_FestivalId",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "FestivalId",
                table: "Filmes");
        }
    }
}
