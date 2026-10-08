using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddConcluidoToFollowUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Concluido",
                table: "FollowUps",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataConclusao",
                table: "FollowUps",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(3204));

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(3068));

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(3072));

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2629));

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2632));

            migrationBuilder.UpdateData(
                table: "Convenio",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(3031));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2908));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2910));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2911));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2912));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2913));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2914));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(3112));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(3117));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(3119));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(3120));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2813));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2816));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2817));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2817));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2819));

            migrationBuilder.UpdateData(
                table: "Sala",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2989));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2863));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2865));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2866));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2868));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2869));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2870));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2872));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2873));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2948));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2952));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2953));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 6, 21, 9, 13, 213, DateTimeKind.Utc).AddTicks(2954));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Concluido",
                table: "FollowUps");

            migrationBuilder.DropColumn(
                name: "DataConclusao",
                table: "FollowUps");

            migrationBuilder.UpdateData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1951));

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1770));

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1776));

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1088));

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1092));

            migrationBuilder.UpdateData(
                table: "Convenio",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1732));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1589));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1595));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1596));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1597));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1597));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1599));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1826));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1831));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1833));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1834));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1357));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1358));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1360));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1361));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1362));

            migrationBuilder.UpdateData(
                table: "Sala",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1690));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1411));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1413));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1415));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1416));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1417));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1418));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1420));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1421));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1650));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1651));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1652));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 10, 5, 17, 46, 45, 878, DateTimeKind.Utc).AddTicks(1653));
        }
    }
}
