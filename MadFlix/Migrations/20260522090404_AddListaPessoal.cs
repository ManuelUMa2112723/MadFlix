using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadFlix.Migrations
{
    /// <inheritdoc />
    public partial class AddListaPessoal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListasPessoais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    UtilizadorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListasPessoais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListasPessoais_Utilizadores_UtilizadorId",
                        column: x => x.UtilizadorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListasPessoaisFilmes",
                columns: table => new
                {
                    ListaPessoalId = table.Column<int>(type: "INTEGER", nullable: false),
                    FilmeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListasPessoaisFilmes", x => new { x.ListaPessoalId, x.FilmeId });
                    table.ForeignKey(
                        name: "FK_ListasPessoaisFilmes_Filmes_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "Filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListasPessoaisFilmes_ListasPessoais_ListaPessoalId",
                        column: x => x.ListaPessoalId,
                        principalTable: "ListasPessoais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListasPessoais_UtilizadorId",
                table: "ListasPessoais",
                column: "UtilizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ListasPessoaisFilmes_FilmeId",
                table: "ListasPessoaisFilmes",
                column: "FilmeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListasPessoaisFilmes");

            migrationBuilder.DropTable(
                name: "ListasPessoais");
        }
    }
}
