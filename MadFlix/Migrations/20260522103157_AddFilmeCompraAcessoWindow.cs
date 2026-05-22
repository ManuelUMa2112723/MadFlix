using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadFlix.Migrations
{
    /// <inheritdoc />
    public partial class AddFilmeCompraAcessoWindow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FilmeCompras_UtilizadorId_FilmeId",
                table: "FilmeCompras");

            migrationBuilder.AddColumn<DateTime>(
                name: "FimAcessoUtc",
                table: "FilmeCompras",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "InicioAcessoUtc",
                table: "FilmeCompras",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TipoAcesso",
                table: "FilmeCompras",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("""
                UPDATE FilmeCompras
                SET InicioAcessoUtc = CompradoEmUtc,
                    FimAcessoUtc = datetime(CompradoEmUtc, '+5 hours')
                WHERE InicioAcessoUtc = '0001-01-01 00:00:00'
                   OR FimAcessoUtc = '0001-01-01 00:00:00'
                """);

            migrationBuilder.CreateIndex(
                name: "IX_FilmeCompras_UtilizadorId_FilmeId",
                table: "FilmeCompras",
                columns: new[] { "UtilizadorId", "FilmeId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FilmeCompras_UtilizadorId_FilmeId",
                table: "FilmeCompras");

            migrationBuilder.DropColumn(
                name: "FimAcessoUtc",
                table: "FilmeCompras");

            migrationBuilder.DropColumn(
                name: "InicioAcessoUtc",
                table: "FilmeCompras");

            migrationBuilder.DropColumn(
                name: "TipoAcesso",
                table: "FilmeCompras");

            migrationBuilder.CreateIndex(
                name: "IX_FilmeCompras_UtilizadorId_FilmeId",
                table: "FilmeCompras",
                columns: new[] { "UtilizadorId", "FilmeId" },
                unique: true);
        }
    }
}
