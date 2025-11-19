using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AdicionarPacotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_Paciente_PacoteId",
                table: "Agenda");

            migrationBuilder.DropIndex(
                name: "IX_Agenda_PacoteId",
                table: "Agenda");

            migrationBuilder.AddColumn<int>(
                name: "PacotePacienteId",
                table: "Agenda",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pacotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpresaId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioCriacaoId = table.Column<string>(type: "text", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioAlteracaoId = table.Column<string>(type: "text", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ProcedimentoId = table.Column<int>(type: "integer", nullable: false),
                    QuantidadeSessoes = table.Column<int>(type: "integer", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    CentroCustoId = table.Column<int>(type: "integer", nullable: true),
                    Observacao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pacotes_CentroCusto_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pacotes_Procedimento_ProcedimentoId",
                        column: x => x.ProcedimentoId,
                        principalTable: "Procedimento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PacotesPacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpresaId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioCriacaoId = table.Column<string>(type: "text", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioAlteracaoId = table.Column<string>(type: "text", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    PacoteId = table.Column<int>(type: "integer", nullable: false),
                    PacienteId = table.Column<int>(type: "integer", nullable: false),
                    FinanceiroId = table.Column<int>(type: "integer", nullable: true),
                    QuantidadeTotal = table.Column<int>(type: "integer", nullable: false),
                    QuantidadeUsada = table.Column<int>(type: "integer", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Observacao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacotesPacientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PacotesPacientes_Financ_Receber_FinanceiroId",
                        column: x => x.FinanceiroId,
                        principalTable: "Financ_Receber",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PacotesPacientes_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacotesPacientes_Pacotes_PacoteId",
                        column: x => x.PacoteId,
                        principalTable: "Pacotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PacotesUsos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpresaId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioCriacaoId = table.Column<string>(type: "text", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioAlteracaoId = table.Column<string>(type: "text", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    PacotePacienteId = table.Column<int>(type: "integer", nullable: false),
                    AgendaId = table.Column<int>(type: "integer", nullable: false),
                    PacienteUtilizadoId = table.Column<int>(type: "integer", nullable: false),
                    DataUso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Observacao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacotesUsos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PacotesUsos_Agenda_AgendaId",
                        column: x => x.AgendaId,
                        principalTable: "Agenda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacotesUsos_Paciente_PacienteUtilizadoId",
                        column: x => x.PacienteUtilizadoId,
                        principalTable: "Paciente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacotesUsos_PacotesPacientes_PacotePacienteId",
                        column: x => x.PacotePacienteId,
                        principalTable: "PacotesPacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1969) });

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1974) });

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1447) });

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1452) });

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
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1760) });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1762) });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1764) });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1765) });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1767) });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1768) });

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(2025) });

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(2030) });

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(2032) });

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(2033) });

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
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1855) });

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
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1812) });

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1813) });

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ativo", "DataCriacao" },
                values: new object[] { true, new DateTime(2025, 10, 23, 19, 52, 39, 812, DateTimeKind.Utc).AddTicks(1815) });

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_PacotePacienteId",
                table: "Agenda",
                column: "PacotePacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacotes_CentroCustoId",
                table: "Pacotes",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacotes_ProcedimentoId",
                table: "Pacotes",
                column: "ProcedimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_PacotesPacientes_FinanceiroId",
                table: "PacotesPacientes",
                column: "FinanceiroId");

            migrationBuilder.CreateIndex(
                name: "IX_PacotesPacientes_PacienteId",
                table: "PacotesPacientes",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PacotesPacientes_PacoteId",
                table: "PacotesPacientes",
                column: "PacoteId");

            migrationBuilder.CreateIndex(
                name: "IX_PacotesUsos_AgendaId",
                table: "PacotesUsos",
                column: "AgendaId");

            migrationBuilder.CreateIndex(
                name: "IX_PacotesUsos_PacienteUtilizadoId",
                table: "PacotesUsos",
                column: "PacienteUtilizadoId");

            migrationBuilder.CreateIndex(
                name: "IX_PacotesUsos_PacotePacienteId",
                table: "PacotesUsos",
                column: "PacotePacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_PacotesPacientes_PacotePacienteId",
                table: "Agenda",
                column: "PacotePacienteId",
                principalTable: "PacotesPacientes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_PacotesPacientes_PacotePacienteId",
                table: "Agenda");

            migrationBuilder.DropTable(
                name: "PacotesUsos");

            migrationBuilder.DropTable(
                name: "PacotesPacientes");

            migrationBuilder.DropTable(
                name: "Pacotes");

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

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_PacoteId",
                table: "Agenda",
                column: "PacoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Paciente_PacoteId",
                table: "Agenda",
                column: "PacoteId",
                principalTable: "Paciente",
                principalColumn: "Id");
        }
    }
}
