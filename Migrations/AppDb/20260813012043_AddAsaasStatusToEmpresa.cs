using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddAsaasStatusToEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AsaasErroDetalhe",
                table: "Empresas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AsaasInvoiceUrl",
                table: "Empresas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AsaasPaymentId",
                table: "Empresas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AsaasStatus",
                table: "Empresas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AsaasUltimaTentativa",
                table: "Empresas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1501));

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1506));

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(990));

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(993));

            migrationBuilder.UpdateData(
                table: "Convenio",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1416));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1297));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1299));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1300));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1301));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1302));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1303));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1557));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1562));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1563));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1564));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1184));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1186));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1187));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1188));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1189));

            migrationBuilder.UpdateData(
                table: "Sala",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1378));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1238));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1242));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1244));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1245));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1246));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1247));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1248));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1250));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1345));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1346));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1347));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 13, 1, 20, 42, 397, DateTimeKind.Utc).AddTicks(1348));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AsaasErroDetalhe",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "AsaasInvoiceUrl",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "AsaasPaymentId",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "AsaasStatus",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "AsaasUltimaTentativa",
                table: "Empresas");

            migrationBuilder.UpdateData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7474));

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7369));

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7375));

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(6415));

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(6420));

            migrationBuilder.UpdateData(
                table: "Convenio",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7278));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7096));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7099));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7101));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7102));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7104));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7105));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7431));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7436));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7438));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7440));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(6831));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(6834));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(6836));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(6839));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(6840));

            migrationBuilder.UpdateData(
                table: "Sala",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7232));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(6976));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(6982));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(6984));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(6985));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(6987));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(6989));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(6991));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(6993));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7185));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7187));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7189));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 7, 25, 18, 4, 45, 895, DateTimeKind.Utc).AddTicks(7189));
        }
    }
}
