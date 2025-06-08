using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class attempresaEgeral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DespesasFixas_Financ_Pagar_FinancPagarId",
                table: "DespesasFixas");

            migrationBuilder.DropForeignKey(
                name: "FK_Financ_Pagar_DespesasFixas_DespesaFixaId",
                table: "Financ_Pagar");

            migrationBuilder.DropIndex(
                name: "IX_DespesasFixas_FinancPagarId",
                table: "DespesasFixas");

            migrationBuilder.DropColumn(
                name: "FinancPagarId",
                table: "DespesasFixas");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "DespesasFixas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_Pagar_DespesasFixas_DespesaFixaId",
                table: "Financ_Pagar",
                column: "DespesaFixaId",
                principalTable: "DespesasFixas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Financ_Pagar_DespesasFixas_DespesaFixaId",
                table: "Financ_Pagar");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "DespesasFixas");

            migrationBuilder.AddColumn<int>(
                name: "FinancPagarId",
                table: "DespesasFixas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DespesasFixas_FinancPagarId",
                table: "DespesasFixas",
                column: "FinancPagarId");

            migrationBuilder.AddForeignKey(
                name: "FK_DespesasFixas_Financ_Pagar_FinancPagarId",
                table: "DespesasFixas",
                column: "FinancPagarId",
                principalTable: "Financ_Pagar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_Pagar_DespesasFixas_DespesaFixaId",
                table: "Financ_Pagar",
                column: "DespesaFixaId",
                principalTable: "DespesasFixas",
                principalColumn: "Id");
        }
    }
}
