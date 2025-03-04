using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class RecorrenciaPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorStatus",
                table: "Agenda");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Agenda",
                newName: "Titulo");

            migrationBuilder.RenameColumn(
                name: "FinanceiroId",
                table: "Agenda",
                newName: "StatusId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFimRecorrencia",
                table: "Paciente",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SalaId",
                table: "Agenda",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ProfissionalId",
                table: "Agenda",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PacienteId",
                table: "Agenda",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFimRecorrencia",
                table: "Agenda",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int[]>(
                name: "DiasRecorrencia",
                table: "Agenda",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinancReceberId",
                table: "Agenda",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecorrenciaPaciente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PacienteId = table.Column<int>(type: "integer", nullable: true),
                    DiaSemana = table.Column<int>(type: "integer", nullable: true),
                    HoraInicio = table.Column<TimeSpan>(type: "interval", nullable: true),
                    HoraFim = table.Column<TimeSpan>(type: "interval", nullable: true),
                    ProfissionalId = table.Column<int>(type: "integer", nullable: true),
                    SalaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecorrenciaPaciente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecorrenciaPacienteDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiaSemana = table.Column<int>(type: "integer", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    HoraFim = table.Column<TimeSpan>(type: "interval", nullable: false),
                    ProfissionalId = table.Column<int>(type: "integer", nullable: false),
                    SalaId = table.Column<int>(type: "integer", nullable: false),
                    PacienteModelId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecorrenciaPacienteDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecorrenciaPacienteDto_Paciente_PacienteModelId",
                        column: x => x.PacienteModelId,
                        principalTable: "Paciente",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_FinancReceberId",
                table: "Agenda",
                column: "FinancReceberId");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_PacienteId",
                table: "Agenda",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_PacoteId",
                table: "Agenda",
                column: "PacoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_ProfissionalId",
                table: "Agenda",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_StatusId",
                table: "Agenda",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RecorrenciaPacienteDto_PacienteModelId",
                table: "RecorrenciaPacienteDto",
                column: "PacienteModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Financ_Receber_FinancReceberId",
                table: "Agenda",
                column: "FinancReceberId",
                principalTable: "Financ_Receber",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Paciente_PacienteId",
                table: "Agenda",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Paciente_PacoteId",
                table: "Agenda",
                column: "PacoteId",
                principalTable: "Paciente",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Profissional_ProfissionalId",
                table: "Agenda",
                column: "ProfissionalId",
                principalTable: "Profissional",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Status_StatusId",
                table: "Agenda",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_Financ_Receber_FinancReceberId",
                table: "Agenda");

            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_Paciente_PacienteId",
                table: "Agenda");

            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_Paciente_PacoteId",
                table: "Agenda");

            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_Profissional_ProfissionalId",
                table: "Agenda");

            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_Status_StatusId",
                table: "Agenda");

            migrationBuilder.DropTable(
                name: "RecorrenciaPaciente");

            migrationBuilder.DropTable(
                name: "RecorrenciaPacienteDto");

            migrationBuilder.DropIndex(
                name: "IX_Agenda_FinancReceberId",
                table: "Agenda");

            migrationBuilder.DropIndex(
                name: "IX_Agenda_PacienteId",
                table: "Agenda");

            migrationBuilder.DropIndex(
                name: "IX_Agenda_PacoteId",
                table: "Agenda");

            migrationBuilder.DropIndex(
                name: "IX_Agenda_ProfissionalId",
                table: "Agenda");

            migrationBuilder.DropIndex(
                name: "IX_Agenda_StatusId",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "DataFimRecorrencia",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "DataFimRecorrencia",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "DiasRecorrencia",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "FinancReceberId",
                table: "Agenda");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Agenda",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Agenda",
                newName: "FinanceiroId");

            migrationBuilder.AlterColumn<int>(
                name: "SalaId",
                table: "Agenda",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProfissionalId",
                table: "Agenda",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PacienteId",
                table: "Agenda",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorStatus",
                table: "Agenda",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
