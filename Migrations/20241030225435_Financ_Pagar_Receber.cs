using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class Financ_Pagar_Receber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Financ_Pagar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrigem = table.Column<int>(type: "int", nullable: false),
                    NrDocto = table.Column<int>(type: "int", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValorOriginal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotaFiscal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parcela = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classificacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Juros = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Multa = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FornecedorId = table.Column<int>(type: "int", nullable: true),
                    CentroCustoId = table.Column<int>(type: "int", nullable: true),
                    TipoPagamentoId = table.Column<int>(type: "int", nullable: true),
                    FormaPagamentoId = table.Column<int>(type: "int", nullable: true),
                    BancoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financ_Pagar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Financ_Pagar_Banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Banco",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Pagar_CentroCusto_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Pagar_FormaPagamento_FormaPagamentoId",
                        column: x => x.FormaPagamentoId,
                        principalTable: "FormaPagamento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Pagar_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Pagar_TipoPagamento_TipoPagamentoId",
                        column: x => x.TipoPagamentoId,
                        principalTable: "TipoPagamento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Financ_Receber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrigem = table.Column<int>(type: "int", nullable: false),
                    NrDocto = table.Column<int>(type: "int", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValorOriginal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotaFiscal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parcela = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classificacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Juros = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Multa = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FornecedorId = table.Column<int>(type: "int", nullable: true),
                    CentroCustoId = table.Column<int>(type: "int", nullable: true),
                    TipoPagamentoId = table.Column<int>(type: "int", nullable: true),
                    FormaPagamentoId = table.Column<int>(type: "int", nullable: true),
                    BancoId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_Financ_Receber_FormaPagamento_FormaPagamentoId",
                        column: x => x.FormaPagamentoId,
                        principalTable: "FormaPagamento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Receber_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Receber_TipoPagamento_TipoPagamentoId",
                        column: x => x.TipoPagamentoId,
                        principalTable: "TipoPagamento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Profissional",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConselhoId = table.Column<int>(type: "int", nullable: false),
                    RegistroConselho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UfConselho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfissaoId = table.Column<int>(type: "int", nullable: false),
                    Cbo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rqe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cnes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissional", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Permissao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    ProfissionalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Profissional_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissional",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Pagar_BancoId",
                table: "Financ_Pagar",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Pagar_CentroCustoId",
                table: "Financ_Pagar",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Pagar_FormaPagamentoId",
                table: "Financ_Pagar",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Pagar_FornecedorId",
                table: "Financ_Pagar",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Pagar_TipoPagamentoId",
                table: "Financ_Pagar",
                column: "TipoPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_BancoId",
                table: "Financ_Receber",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_CentroCustoId",
                table: "Financ_Receber",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_FormaPagamentoId",
                table: "Financ_Receber",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_FornecedorId",
                table: "Financ_Receber",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_TipoPagamentoId",
                table: "Financ_Receber",
                column: "TipoPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_ProfissionalId",
                table: "Usuario",
                column: "ProfissionalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Financ_Pagar");

            migrationBuilder.DropTable(
                name: "Financ_Receber");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Profissional");
        }
    }
}
