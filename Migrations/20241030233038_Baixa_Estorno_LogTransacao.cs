using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class Baixa_Estorno_LogTransacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Financ_Pagar_Banco_BancoId",
                table: "Financ_Pagar");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorPago",
                table: "Financ_Pagar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorOriginal",
                table: "Financ_Pagar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BancoId",
                table: "Financ_Pagar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "HistoricoTransacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BancoId = table.Column<int>(type: "int", nullable: false),
                    DataTransacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoTransacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Referencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoTransacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoTransacao_Banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Banco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricoTransacao_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoTransacao_BancoId",
                table: "HistoricoTransacao",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoTransacao_UsuarioId",
                table: "HistoricoTransacao",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_Pagar_Banco_BancoId",
                table: "Financ_Pagar",
                column: "BancoId",
                principalTable: "Banco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Financ_Pagar_Banco_BancoId",
                table: "Financ_Pagar");

            migrationBuilder.DropTable(
                name: "HistoricoTransacao");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorPago",
                table: "Financ_Pagar",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorOriginal",
                table: "Financ_Pagar",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "BancoId",
                table: "Financ_Pagar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_Pagar_Banco_BancoId",
                table: "Financ_Pagar",
                column: "BancoId",
                principalTable: "Banco",
                principalColumn: "Id");
        }
    }
}
