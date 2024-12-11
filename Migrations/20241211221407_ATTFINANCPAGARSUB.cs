using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class ATTFINANCPAGARSUB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Financ_Pagar_Banco_BancoId",
                table: "Financ_Pagar");

            migrationBuilder.DropForeignKey(
                name: "FK_Financ_Pagar_FormaPagamento_FormaPagamentoId",
                table: "Financ_Pagar");

            migrationBuilder.DropForeignKey(
                name: "FK_Financ_Pagar_TipoPagamento_TipoPagamentoId",
                table: "Financ_Pagar");

            migrationBuilder.DropIndex(
                name: "IX_Financ_Pagar_FormaPagamentoId",
                table: "Financ_Pagar");

            migrationBuilder.DropColumn(
                name: "DataPagamento",
                table: "Financ_Pagar");

            migrationBuilder.DropColumn(
                name: "DataVencimento",
                table: "Financ_Pagar");

            migrationBuilder.DropColumn(
                name: "Desconto",
                table: "Financ_Pagar");

            migrationBuilder.DropColumn(
                name: "FormaPagamentoId",
                table: "Financ_Pagar");

            migrationBuilder.DropColumn(
                name: "Juros",
                table: "Financ_Pagar");

            migrationBuilder.RenameColumn(
                name: "TipoPagamentoId",
                table: "Financ_Pagar",
                newName: "PacienteId");

            migrationBuilder.RenameColumn(
                name: "Multa",
                table: "Financ_Pagar",
                newName: "Valor");

            migrationBuilder.RenameIndex(
                name: "IX_Financ_Pagar_TipoPagamentoId",
                table: "Financ_Pagar",
                newName: "IX_Financ_Pagar_PacienteId");

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

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Financ_Pagar",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "Financ_Pagar",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "NrDocto",
                table: "Financ_Pagar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NotaFiscal",
                table: "Financ_Pagar",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "IdOrigem",
                table: "Financ_Pagar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Financ_Pagar",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Classificacao",
                table: "Financ_Pagar",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "BancoId",
                table: "Financ_Pagar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Financ_PagarSubModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    financPagarId = table.Column<int>(type: "int", nullable: true),
                    Parcela = table.Column<int>(type: "int", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Juros = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Multa = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TipoPagamentoId = table.Column<int>(type: "int", nullable: true),
                    FormaPagamentoId = table.Column<int>(type: "int", nullable: true),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financ_PagarSubModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Financ_PagarSubModel_Financ_Pagar_financPagarId",
                        column: x => x.financPagarId,
                        principalTable: "Financ_Pagar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_PagarSubModel_FormaPagamento_FormaPagamentoId",
                        column: x => x.FormaPagamentoId,
                        principalTable: "FormaPagamento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_PagarSubModel_TipoPagamento_TipoPagamentoId",
                        column: x => x.TipoPagamentoId,
                        principalTable: "TipoPagamento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Financ_PagarSubModel_financPagarId",
                table: "Financ_PagarSubModel",
                column: "financPagarId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_PagarSubModel_FormaPagamentoId",
                table: "Financ_PagarSubModel",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_PagarSubModel_TipoPagamentoId",
                table: "Financ_PagarSubModel",
                column: "TipoPagamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_Pagar_Banco_BancoId",
                table: "Financ_Pagar",
                column: "BancoId",
                principalTable: "Banco",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_Pagar_Paciente_PacienteId",
                table: "Financ_Pagar",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Financ_Pagar_Banco_BancoId",
                table: "Financ_Pagar");

            migrationBuilder.DropForeignKey(
                name: "FK_Financ_Pagar_Paciente_PacienteId",
                table: "Financ_Pagar");

            migrationBuilder.DropTable(
                name: "Financ_PagarSubModel");

            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Financ_Pagar",
                newName: "Multa");

            migrationBuilder.RenameColumn(
                name: "PacienteId",
                table: "Financ_Pagar",
                newName: "TipoPagamentoId");

            migrationBuilder.RenameIndex(
                name: "IX_Financ_Pagar_PacienteId",
                table: "Financ_Pagar",
                newName: "IX_Financ_Pagar_TipoPagamentoId");

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

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Financ_Pagar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "Financ_Pagar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NrDocto",
                table: "Financ_Pagar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NotaFiscal",
                table: "Financ_Pagar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdOrigem",
                table: "Financ_Pagar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Financ_Pagar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Classificacao",
                table: "Financ_Pagar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
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

            migrationBuilder.AddColumn<DateTime>(
                name: "DataPagamento",
                table: "Financ_Pagar",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataVencimento",
                table: "Financ_Pagar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Desconto",
                table: "Financ_Pagar",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormaPagamentoId",
                table: "Financ_Pagar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Juros",
                table: "Financ_Pagar",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Pagar_FormaPagamentoId",
                table: "Financ_Pagar",
                column: "FormaPagamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_Pagar_Banco_BancoId",
                table: "Financ_Pagar",
                column: "BancoId",
                principalTable: "Banco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_Pagar_FormaPagamento_FormaPagamentoId",
                table: "Financ_Pagar",
                column: "FormaPagamentoId",
                principalTable: "FormaPagamento",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_Pagar_TipoPagamento_TipoPagamentoId",
                table: "Financ_Pagar",
                column: "TipoPagamentoId",
                principalTable: "TipoPagamento",
                principalColumn: "Id");
        }
    }
}
