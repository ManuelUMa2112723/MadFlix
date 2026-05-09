using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadFlix.Migrations
{
    /// <inheritdoc />
    public partial class AddCarrinho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carrinhos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UtilizadorId = table.Column<int>(type: "INTEGER", nullable: false),
                    CriadoEmUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrinhos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carrinhos_Utilizadores_UtilizadorId",
                        column: x => x.UtilizadorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrinhoItens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CarrinhoId = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoAcesso = table.Column<int>(type: "INTEGER", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: false),
                    InicioAcessoUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FimAcessoUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AdicionadoEmUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FestivalId = table.Column<int>(type: "INTEGER", nullable: true),
                    FilmeId = table.Column<int>(type: "INTEGER", nullable: true),
                    SessaoId = table.Column<int>(type: "INTEGER", nullable: true),
                    DiaFestival = table.Column<DateTime>(type: "TEXT", nullable: true),
                    HorasAluguer = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrinhoItens_Carrinhos_CarrinhoId",
                        column: x => x.CarrinhoId,
                        principalTable: "Carrinhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrinhoItens_Festivais_FestivalId",
                        column: x => x.FestivalId,
                        principalTable: "Festivais",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CarrinhoItens_Filmes_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "Filmes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItens_CarrinhoId",
                table: "CarrinhoItens",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItens_FestivalId",
                table: "CarrinhoItens",
                column: "FestivalId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItens_FilmeId",
                table: "CarrinhoItens",
                column: "FilmeId");

            migrationBuilder.CreateIndex(
                name: "IX_Carrinhos_UtilizadorId",
                table: "Carrinhos",
                column: "UtilizadorId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoItens");

            migrationBuilder.DropTable(
                name: "Carrinhos");
        }
    }
}
