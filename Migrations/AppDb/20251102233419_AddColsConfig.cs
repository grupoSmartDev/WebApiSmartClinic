using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddColsConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cep",
                table: "Empresas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Empresas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Empresas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Empresas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InscricaoEstadual",
                table: "Empresas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InscricaoMunicipal",
                table: "Empresas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SiteOuRedeSocial",
                table: "Empresas",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7572));

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7412) });

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7419) });

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(6719) });

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(6724) });

            migrationBuilder.UpdateData(
                table: "Convenio",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7366));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7184) });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7186) });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7188) });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7190) });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7191) });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7193) });

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7466) });

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7471) });

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7474) });

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7521) });

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7005));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7009));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7011));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7012));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7014));

            migrationBuilder.UpdateData(
                table: "Sala",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7312) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7072));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7076));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7078));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7080));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7082));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7084));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7085));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7087));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7258) });

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7260) });

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7261) });

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 11, 2, 23, 34, 17, 916, DateTimeKind.Utc).AddTicks(7263) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cep",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "InscricaoEstadual",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "InscricaoMunicipal",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "SiteOuRedeSocial",
                table: "Empresas");

            migrationBuilder.UpdateData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(8176));

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7998) });

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(8004) });

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7384) });

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7387) });

            migrationBuilder.UpdateData(
                table: "Convenio",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7951));

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7808) });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7810) });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7811) });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7812) });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7814) });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7815) });

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(8119) });

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(8125) });

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(8127) });

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(8132) });

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7675));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7679));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7681));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7682));

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7683));

            migrationBuilder.UpdateData(
                table: "Sala",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7908) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7737));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7740));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7742));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7744));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7746));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7747));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7750));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataCriacao",
                value: new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7752));

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7864) });

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7866) });

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7868) });

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { false, new DateTime(2025, 9, 30, 13, 17, 28, 585, DateTimeKind.Utc).AddTicks(7869) });
        }
    }
}
