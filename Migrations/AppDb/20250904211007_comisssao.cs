using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class comisssao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_Paciente_PacienteId",
                table: "Agenda");

            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_Profissional_ProfissionalId",
                table: "Agenda");

            migrationBuilder.AddColumn<string>(
                name: "TipoComissao",
                table: "Profissional",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorComissao",
                table: "Profissional",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Paciente",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Paciente",
                type: "timestamp with time zone",
                nullable: true);


            migrationBuilder.CreateTable(
                name: "Comissoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProfissionalId = table.Column<int>(type: "integer", nullable: false),
                    AgendamentoId = table.Column<int>(type: "integer", nullable: false),
                    DataAgendamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TipoComissaoUtilizado = table.Column<string>(type: "text", nullable: true),
                    PercentualOuValor = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorBase = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorComissao = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    NomePaciente = table.Column<string>(type: "text", nullable: true),
                    NomePlano = table.Column<string>(type: "text", nullable: true),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioPagamento = table.Column<string>(type: "text", nullable: true),
                    DataCalculo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comissoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comissoes_Agenda_AgendamentoId",
                        column: x => x.AgendamentoId,
                        principalTable: "Agenda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comissoes_Profissional_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissional",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PacientePlanoHistoricos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PacienteId = table.Column<int>(type: "integer", nullable: false),
                    PlanoId = table.Column<int>(type: "integer", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AulasContratadas = table.Column<int>(type: "integer", nullable: false),
                    AulasUtilizadas = table.Column<int>(type: "integer", nullable: false),
                    ValorPago = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    MotivoFinalizacao = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacientePlanoHistoricos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PacientePlanoHistoricos_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacientePlanoHistoricos_Plano_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "Plano",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comissoes_AgendamentoId",
                table: "Comissoes",
                column: "AgendamentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comissoes_ProfissionalId",
                table: "Comissoes",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_PacientePlanoHistoricos_PacienteId",
                table: "PacientePlanoHistoricos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PacientePlanoHistoricos_PlanoId",
                table: "PacientePlanoHistoricos",
                column: "PlanoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Paciente_PacienteId",
                table: "Agenda",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Profissional_ProfissionalId",
                table: "Agenda",
                column: "ProfissionalId",
                principalTable: "Profissional",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_Paciente_PacienteId",
                table: "Agenda");

            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_Profissional_ProfissionalId",
                table: "Agenda");

            migrationBuilder.DropTable(
                name: "Comissoes");

            migrationBuilder.DropTable(
                name: "PacientePlanoHistoricos");

            migrationBuilder.DropColumn(
                name: "TipoComissao",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "ValorComissao",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Paciente");

           

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Paciente_PacienteId",
                table: "Agenda",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Profissional_ProfissionalId",
                table: "Agenda",
                column: "ProfissionalId",
                principalTable: "Profissional",
                principalColumn: "Id");
        }
    }
}
