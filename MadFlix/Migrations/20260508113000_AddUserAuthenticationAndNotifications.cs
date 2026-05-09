using MadFlix.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadFlix.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20260508113000_AddUserAuthenticationAndNotifications")]
    public partial class AddUserAuthenticationAndNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeUtilizador = table.Column<string>(type: "TEXT", nullable: false),
                    NomeUtilizadorNormalizado = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    EmailNormalizado = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    CriadoEmUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilmeCompras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UtilizadorId = table.Column<int>(type: "INTEGER", nullable: false),
                    FilmeId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompradoEmUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmeCompras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilmeCompras_Filmes_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "Filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmeCompras_Utilizadores_UtilizadorId",
                        column: x => x.UtilizadorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmeVisualizacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UtilizadorId = table.Column<int>(type: "INTEGER", nullable: false),
                    FilmeId = table.Column<int>(type: "INTEGER", nullable: false),
                    VisualizadoEmUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmeVisualizacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilmeVisualizacoes_Filmes_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "Filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmeVisualizacoes_Utilizadores_UtilizadorId",
                        column: x => x.UtilizadorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmeCompras_FilmeId",
                table: "FilmeCompras",
                column: "FilmeId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmeCompras_UtilizadorId_FilmeId",
                table: "FilmeCompras",
                columns: new[] { "UtilizadorId", "FilmeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilmeVisualizacoes_FilmeId",
                table: "FilmeVisualizacoes",
                column: "FilmeId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmeVisualizacoes_UtilizadorId_FilmeId",
                table: "FilmeVisualizacoes",
                columns: new[] { "UtilizadorId", "FilmeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_EmailNormalizado",
                table: "Utilizadores",
                column: "EmailNormalizado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_NomeUtilizadorNormalizado",
                table: "Utilizadores",
                column: "NomeUtilizadorNormalizado",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmeCompras");

            migrationBuilder.DropTable(
                name: "FilmeVisualizacoes");

            migrationBuilder.DropTable(
                name: "Utilizadores");
        }
    }
}
