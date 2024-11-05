using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class attProfissional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BancoAgencia",
                table: "Profissional",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BancoConta",
                table: "Profissional",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BancoCpfTitular",
                table: "Profissional",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BancoNome",
                table: "Profissional",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BancoTipoConta",
                table: "Profissional",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChavePix",
                table: "Profissional",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Profissional",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "EhUsuario",
                table: "Profissional",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TipoPagamento",
                table: "Profissional",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BancoAgencia",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "BancoConta",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "BancoCpfTitular",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "BancoNome",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "BancoTipoConta",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "ChavePix",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "EhUsuario",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "TipoPagamento",
                table: "Profissional");
        }
    }
}
