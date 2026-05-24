using System;
using MadFlix.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadFlix.Migrations
{
    /// <inheritdoc />
    [DbContext(typeof(AppDbContext))]
    [Migration("20260524120000_AddFestivalFavoriteVotes")]
    public partial class AddFestivalFavoriteVotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FestivalFilmeVotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FestivalId = table.Column<int>(type: "INTEGER", nullable: false),
                    FilmeId = table.Column<int>(type: "INTEGER", nullable: false),
                    UtilizadorId = table.Column<int>(type: "INTEGER", nullable: false),
                    CriadoEmUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AtualizadoEmUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FestivalFilmeVotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FestivalFilmeVotos_Festivais_FestivalId",
                        column: x => x.FestivalId,
                        principalTable: "Festivais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FestivalFilmeVotos_Filmes_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "Filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FestivalFilmeVotos_Utilizadores_UtilizadorId",
                        column: x => x.UtilizadorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FestivalFilmeVotos_FestivalId_FilmeId",
                table: "FestivalFilmeVotos",
                columns: new[] { "FestivalId", "FilmeId" });

            migrationBuilder.CreateIndex(
                name: "IX_FestivalFilmeVotos_FestivalId_UtilizadorId",
                table: "FestivalFilmeVotos",
                columns: new[] { "FestivalId", "UtilizadorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FestivalFilmeVotos_FilmeId",
                table: "FestivalFilmeVotos",
                column: "FilmeId");

            migrationBuilder.CreateIndex(
                name: "IX_FestivalFilmeVotos_UtilizadorId",
                table: "FestivalFilmeVotos",
                column: "UtilizadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FestivalFilmeVotos");
        }
    }
}
