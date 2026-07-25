using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class CorrigirFKPacoteAgenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_PacotesPacientes_PacotePacienteId",
                table: "Agenda");

            migrationBuilder.DropIndex(
                name: "IX_Agenda_PacotePacienteId",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "PacotePacienteId",
                table: "Agenda");

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

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_PacoteId",
                table: "Agenda",
                column: "PacoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_PacotesPacientes_PacoteId",
                table: "Agenda",
                column: "PacoteId",
                principalTable: "PacotesPacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_PacotesPacientes_PacoteId",
                table: "Agenda");

            migrationBuilder.DropIndex(
                name: "IX_Agenda_PacoteId",
                table: "Agenda");

            migrationBuilder.AddColumn<int>(
                name: "PacotePacienteId",
                table: "Agenda",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(2072));

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1969));

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1974));

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1447));

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1452));

            migrationBuilder.UpdateData(
                table: "Convenio",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1929));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1760));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1762));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1764));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1765));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1767));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1768));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(2025));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(2030));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(2032));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(2033));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1646));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1647));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1648));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1650));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1651));

            migrationBuilder.UpdateData(
                table: "Sala",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1855));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1700));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1703));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1705));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1706));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1707));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1710));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1711));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1712));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1810));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1812));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1813));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1815));

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_PacotePacienteId",
                table: "Agenda",
                column: "PacotePacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_PacotesPacientes_PacotePacienteId",
                table: "Agenda",
                column: "PacotePacienteId",
                principalTable: "PacotesPacientes",
                principalColumn: "Id");
        }
    }
}
