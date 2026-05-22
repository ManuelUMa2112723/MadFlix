using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadFlix.Migrations
{
    /// <inheritdoc />
    public partial class AddAcessoFestivalCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcessosFestivalCompras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UtilizadorId = table.Column<int>(type: "INTEGER", nullable: false),
                    FestivalId = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoAcesso = table.Column<int>(type: "INTEGER", nullable: false),
                    InicioAcessoUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FimAcessoUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CompradoEmUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcessosFestivalCompras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcessosFestivalCompras_Festivais_FestivalId",
                        column: x => x.FestivalId,
                        principalTable: "Festivais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcessosFestivalCompras_Utilizadores_UtilizadorId",
                        column: x => x.UtilizadorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcessosFestivalCompras_FestivalId",
                table: "AcessosFestivalCompras",
                column: "FestivalId");

            migrationBuilder.CreateIndex(
                name: "IX_AcessosFestivalCompras_UtilizadorId_FestivalId",
                table: "AcessosFestivalCompras",
                columns: new[] { "UtilizadorId", "FestivalId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcessosFestivalCompras");
        }
    }
}
