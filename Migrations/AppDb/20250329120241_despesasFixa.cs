using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class despesasFixa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sobrenome",
                table: "Profissional",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "EhUsuario",
                table: "Profissional",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<int>(
                name: "DespesaFixaId",
                table: "Financ_PagarSub",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanoContaId",
                table: "Financ_Pagar",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DespesasFixas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DiaVencimento = table.Column<int>(type: "integer", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: true),
                    Categoria = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Frequencia = table.Column<int>(type: "integer", nullable: false),
                    FornecedorId = table.Column<int>(type: "integer", nullable: true),
                    PlanoContaId = table.Column<int>(type: "integer", nullable: true),
                    CentroCustoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesasFixas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DespesasFixas_CentroCusto_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DespesasFixas_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DespesasFixas_PlanoConta_PlanoContaId",
                        column: x => x.PlanoContaId,
                        principalTable: "PlanoConta",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Financ_PagarSub_DespesaFixaId",
                table: "Financ_PagarSub",
                column: "DespesaFixaId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Pagar_PlanoContaId",
                table: "Financ_Pagar",
                column: "PlanoContaId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasFixas_CentroCustoId",
                table: "DespesasFixas",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasFixas_FornecedorId",
                table: "DespesasFixas",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasFixas_PlanoContaId",
                table: "DespesasFixas",
                column: "PlanoContaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_Pagar_PlanoConta_PlanoContaId",
                table: "Financ_Pagar",
                column: "PlanoContaId",
                principalTable: "PlanoConta",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_PagarSub_DespesasFixas_DespesaFixaId",
                table: "Financ_PagarSub",
                column: "DespesaFixaId",
                principalTable: "DespesasFixas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Financ_Pagar_PlanoConta_PlanoContaId",
                table: "Financ_Pagar");

            migrationBuilder.DropForeignKey(
                name: "FK_Financ_PagarSub_DespesasFixas_DespesaFixaId",
                table: "Financ_PagarSub");

            migrationBuilder.DropTable(
                name: "DespesasFixas");

            migrationBuilder.DropIndex(
                name: "IX_Financ_PagarSub_DespesaFixaId",
                table: "Financ_PagarSub");

            migrationBuilder.DropIndex(
                name: "IX_Financ_Pagar_PlanoContaId",
                table: "Financ_Pagar");

            migrationBuilder.DropColumn(
                name: "DespesaFixaId",
                table: "Financ_PagarSub");

            migrationBuilder.DropColumn(
                name: "PlanoContaId",
                table: "Financ_Pagar");

            migrationBuilder.AlterColumn<string>(
                name: "Sobrenome",
                table: "Profissional",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "EhUsuario",
                table: "Profissional",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);
        }
    }
}
