using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class filtro_profissional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comissao_Profissional_ProfissionalId",
                table: "Comissao");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Profissional",
                type: "character varying(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProfissionalId",
                table: "Comissoes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ProfissionalId",
                table: "Comissao",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4268));

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4274));

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(3709));

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(3713));

            migrationBuilder.UpdateData(
                table: "Convenio",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4230));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4093));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4095));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4096));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4097));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4098));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4099));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4314));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4318));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4320));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4321));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(3982));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(3983));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(3984));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(3985));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(3986));

            migrationBuilder.UpdateData(
                table: "Sala",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4189));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4038));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4039));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4041));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4042));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4043));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4045));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4046));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4047));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4142));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4143));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4144));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 20, 19, 43, 1, 314, DateTimeKind.Utc).AddTicks(4145));

            migrationBuilder.CreateIndex(
                name: "IX_Profissional_UsuarioId",
                table: "Profissional",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comissao_Profissional_ProfissionalId",
                table: "Comissao",
                column: "ProfissionalId",
                principalTable: "Profissional",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profissional_AspNetUsers_UsuarioId",
                table: "Profissional",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comissao_Profissional_ProfissionalId",
                table: "Comissao");

            migrationBuilder.DropForeignKey(
                name: "FK_Profissional_AspNetUsers_UsuarioId",
                table: "Profissional");

            migrationBuilder.DropIndex(
                name: "IX_Profissional_UsuarioId",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Profissional");

            migrationBuilder.AlterColumn<int>(
                name: "ProfissionalId",
                table: "Comissoes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProfissionalId",
                table: "Comissao",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(2061));

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(2070));

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1494));

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1498));

            migrationBuilder.UpdateData(
                table: "Convenio",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(2024));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1870));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1872));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1873));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1874));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1875));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1876));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(2117));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(2122));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(2123));

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(2124));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1694));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1696));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1697));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1698));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1699));

            migrationBuilder.UpdateData(
                table: "Sala",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1976));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1747));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1749));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1750));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1751));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1753));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1755));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1756));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1757));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1930));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1932));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1933));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1934));

            migrationBuilder.AddForeignKey(
                name: "FK_Comissao_Profissional_ProfissionalId",
                table: "Comissao",
                column: "ProfissionalId",
                principalTable: "Profissional",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
