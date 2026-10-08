using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddCRMModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campanhas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpresaId = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Plataforma = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IdExterno = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    UsuarioCriacaoId = table.Column<string>(type: "text", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioAlteracaoId = table.Column<string>(type: "text", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campanhas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EtapasFunil",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpresaId = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Ordem = table.Column<int>(type: "integer", nullable: false),
                    IsFixa = table.Column<bool>(type: "boolean", nullable: false),
                    Cor = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ScriptSugerido = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    UsuarioCriacaoId = table.Column<string>(type: "text", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioAlteracaoId = table.Column<string>(type: "text", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtapasFunil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpresaId = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Telefone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    CanalOrigem = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CampanhaId = table.Column<int>(type: "integer", nullable: true),
                    FacebookLeadId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EtapaFunilId = table.Column<int>(type: "integer", nullable: false),
                    ProfissionalId = table.Column<int>(type: "integer", nullable: true),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    MotivoPerdaId = table.Column<string>(type: "text", nullable: true),
                    DataUltimaInteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsGanho = table.Column<bool>(type: "boolean", nullable: false),
                    PacienteId = table.Column<int>(type: "integer", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    UsuarioCriacaoId = table.Column<string>(type: "text", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioAlteracaoId = table.Column<string>(type: "text", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leads_Campanhas_CampanhaId",
                        column: x => x.CampanhaId,
                        principalTable: "Campanhas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leads_EtapasFunil_EtapaFunilId",
                        column: x => x.EtapaFunilId,
                        principalTable: "EtapasFunil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leads_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leads_Profissional_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissional",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FollowUps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpresaId = table.Column<int>(type: "integer", nullable: false),
                    LeadId = table.Column<int>(type: "integer", nullable: false),
                    EtapaFunilId = table.Column<int>(type: "integer", nullable: false),
                    Canal = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    MensagemEnviada = table.Column<string>(type: "text", nullable: true),
                    RespostaRecebida = table.Column<string>(type: "text", nullable: true),
                    ProximaAcaoData = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProximaAcaoDescricao = table.Column<string>(type: "text", nullable: true),
                    DataContato = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    UsuarioCriacaoId = table.Column<string>(type: "text", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioAlteracaoId = table.Column<string>(type: "text", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowUps_EtapasFunil_EtapaFunilId",
                        column: x => x.EtapaFunilId,
                        principalTable: "EtapasFunil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FollowUps_Leads_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Leads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "EtapasFunil",
                columns: new[] { "Id", "Ativo", "Cor", "DataAlteracao", "DataCriacao", "EmpresaId", "IsFixa", "Nome", "Ordem", "ScriptSugerido", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[,]
                {
                    { 1, true, "#6B7280", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Novo Lead", 1, null, null, null },
                    { 2, true, "#3B82F6", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "D0", 2, "Olá [Nome]! Vi que você se interessou...", null, null },
                    { 3, true, "#3B82F6", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "D1", 3, null, null, null },
                    { 4, true, "#3B82F6", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "D2", 4, null, null, null },
                    { 5, true, "#F59E0B", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "D5", 5, null, null, null },
                    { 6, true, "#F59E0B", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "D10", 6, null, null, null },
                    { 7, true, "#EF4444", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "D15", 7, null, null, null },
                    { 8, true, "#8B5CF6", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Contato Feito", 8, null, null, null },
                    { 9, true, "#06B6D4", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Agendamento Marcado", 9, null, null, null },
                    { 10, true, "#10B981", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Compareceu", 10, null, null, null },
                    { 11, true, "#F97316", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Proposta Enviada", 11, null, null, null },
                    { 12, true, "#84CC16", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Fechamento", 12, null, null, null },
                    { 13, true, "#22C55E", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Ganho", 13, null, null, null },
                    { 14, true, "#EF4444", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Perdido", 14, null, null, null }
                });

            // O seed grava Ids explícitos (1..14) sem avançar a sequence de identidade —
            // sem isso, a primeira etapa customizada criada tentaria Id = 1 (duplicate key).
            migrationBuilder.Sql(
                "SELECT setval(pg_get_serial_sequence('\"EtapasFunil\"', 'Id'), (SELECT MAX(\"Id\") FROM \"EtapasFunil\"));");

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

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_EtapaFunilId",
                table: "FollowUps",
                column: "EtapaFunilId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_LeadId",
                table: "FollowUps",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_CampanhaId",
                table: "Leads",
                column: "CampanhaId");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_EtapaFunilId",
                table: "Leads",
                column: "EtapaFunilId");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_PacienteId",
                table: "Leads",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_ProfissionalId",
                table: "Leads",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_Telefone",
                table: "Leads",
                column: "Telefone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FollowUps");

            migrationBuilder.DropTable(
                name: "Leads");

            migrationBuilder.DropTable(
                name: "Campanhas");

            migrationBuilder.DropTable(
                name: "EtapasFunil");

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
        }
    }
}
