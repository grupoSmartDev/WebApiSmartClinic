using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class fornecedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ANVISA",
                table: "Fornecedor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Fornecedor",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CRF",
                table: "Fornecedor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoriaFornecedor",
                table: "Fornecedor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Fornecedor",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailRepresentante",
                table: "Fornecedor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EspecialidadeFornecimento",
                table: "Fornecedor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Representante",
                table: "Fornecedor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelefoneRepresentante",
                table: "Fornecedor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AsaasCustomerId",
                table: "Empresas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AsaasSubscriptionId",
                table: "Empresas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PeriodoCobranca",
                table: "Empresas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoSelecionado",
                table: "Empresas",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ANVISA",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "CRF",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "CategoriaFornecedor",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "EmailRepresentante",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "EspecialidadeFornecimento",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "Representante",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "TelefoneRepresentante",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "AsaasCustomerId",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "AsaasSubscriptionId",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "PeriodoCobranca",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "PrecoSelecionado",
                table: "Empresas");
        }
    }
}
