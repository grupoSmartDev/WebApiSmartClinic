using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class tipopagamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Primeiro, drop a tabela existente
            migrationBuilder.DropTable("Financ_Receber");

            // Recrie a tabela com a nova coluna
            migrationBuilder.CreateTable(
                name: "Financ_Receber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdOrigem = table.Column<int>(type: "integer", nullable: true),
                    NrDocto = table.Column<int>(type: "integer", nullable: true),
                    DataEmissao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ValorOriginal = table.Column<decimal>(type: "numeric", nullable: true),
                    ValorPago = table.Column<decimal>(type: "numeric", nullable: true),
                    Valor = table.Column<decimal>(type: "numeric", nullable: true),
                    Parcela = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    NotaFiscal = table.Column<string>(type: "text", nullable: true),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    Classificacao = table.Column<string>(type: "text", nullable: true),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    PacienteId = table.Column<int>(type: "integer", nullable: true),
                    FornecedorId = table.Column<int>(type: "integer", nullable: true),
                    CentroCustoId = table.Column<int>(type: "integer", nullable: true),
                    BancoId = table.Column<int>(type: "integer", nullable: true),
                    TipoPagamentoId = table.Column<int>(type: "integer", nullable: true), // Nova coluna adicionada
                    PlanoContaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financ_Receber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Financ_Receber_Banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Banco",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Receber_CentroCusto_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Receber_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Receber_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Receber_TipoPagamento_TipoPagamentoId",
                        column: x => x.TipoPagamentoId,
                        principalTable: "TipoPagamento",
                        principalColumn: "Id");
                });

            // Recrie os índices
            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_BancoId",
                table: "Financ_Receber",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_CentroCustoId",
                table: "Financ_Receber",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_FornecedorId",
                table: "Financ_Receber",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_PacienteId",
                table: "Financ_Receber",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_TipoPagamentoId",
                table: "Financ_Receber",
                column: "TipoPagamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Método de rollback para reverter as alterações
            migrationBuilder.DropTable("Financ_Receber");

            // Recrie a tabela original sem a coluna TipoPagamentoId
            migrationBuilder.CreateTable(
                name: "Financ_Receber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdOrigem = table.Column<int>(type: "integer", nullable: true),
                    NrDocto = table.Column<int>(type: "integer", nullable: true),
                    DataEmissao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ValorOriginal = table.Column<decimal>(type: "numeric", nullable: true),
                    ValorPago = table.Column<decimal>(type: "numeric", nullable: true),
                    Valor = table.Column<decimal>(type: "numeric", nullable: true),
                    Parcela = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    NotaFiscal = table.Column<string>(type: "text", nullable: true),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    Classificacao = table.Column<string>(type: "text", nullable: true),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    PacienteId = table.Column<int>(type: "integer", nullable: true),
                    FornecedorId = table.Column<int>(type: "integer", nullable: true),
                    CentroCustoId = table.Column<int>(type: "integer", nullable: true),
                    BancoId = table.Column<int>(type: "integer", nullable: true),
                    PlanoContaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financ_Receber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Financ_Receber_Banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Banco",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Receber_CentroCusto_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Receber_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Receber_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "Id");
                });

            // Recrie os índices originais
            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_BancoId",
                table: "Financ_Receber",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_CentroCustoId",
                table: "Financ_Receber",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_FornecedorId",
                table: "Financ_Receber",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_PacienteId",
                table: "Financ_Receber",
                column: "PacienteId");
        }
    }
}