using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddUniqueIndexPacoteUsoAgendaId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PacotesUsos_AgendaId",
                table: "PacotesUsos");

            migrationBuilder.UpdateData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(8094));

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(8008));

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(8012));

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7508));

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7511));

            migrationBuilder.UpdateData(
                table: "Convenio",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7975));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7851));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7852));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7853));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7854));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7855));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7856));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(8052));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(8058));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(8060));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(8061));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7747));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7748));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7749));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7750));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7751));

            migrationBuilder.UpdateData(
                table: "Sala",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7937));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7796));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7798));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7800));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7801));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7802));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7803));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7804));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7805));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7893));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7894));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7895));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2026, 8, 16, 11, 41, 35, 674, DateTimeKind.Utc).AddTicks(7906));

            migrationBuilder.CreateIndex(
                name: "IX_PacotesUsos_AgendaId",
                table: "PacotesUsos",
                column: "AgendaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PacotesUsos_AgendaId",
                table: "PacotesUsos");

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

            migrationBuilder.CreateIndex(
                name: "IX_PacotesUsos_AgendaId",
                table: "PacotesUsos",
                column: "AgendaId");
        }
    }
}
