using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class attficha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ObjetivoTratamento",
                table: "FichaAvaliacao",
                newName: "ObservacoesGerais");

            migrationBuilder.RenameColumn(
                name: "Medicamentos",
                table: "FichaAvaliacao",
                newName: "ObjetivosDoTratamento");

            migrationBuilder.AddColumn<int>(
                name: "FichaAvaliacaoId",
                table: "Paciente",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TipoDor",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "QueixaPrincipal",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "HistoriaPregressa",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "HistoriaAtual",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FrequenciaConsumoAlcool",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Alergias",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Altura",
                table: "FichaAvaliacao",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "AmplitudeMovimento",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssinaturaCliente",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssinaturaProfissional",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvaliacaoPostural",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CirurgiasPrevias",
                table: "FichaAvaliacao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAvaliacao",
                table: "FichaAvaliacao",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DetalheCirurgias",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoencasPreExistentes",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Especialidade",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HistoricoDoencas",
                table: "FichaAvaliacao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "FichaAvaliacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Imc",
                table: "FichaAvaliacao",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medicacao",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MedicacaoUsoContinuo",
                table: "FichaAvaliacao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "FichaAvaliacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Peso",
                table: "FichaAvaliacao",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ProfissionalId",
                table: "FichaAvaliacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_FichaAvaliacaoId",
                table: "Paciente",
                column: "FichaAvaliacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_FichaAvaliacao_PacienteId",
                table: "FichaAvaliacao",
                column: "PacienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FichaAvaliacao_ProfissionalId",
                table: "FichaAvaliacao",
                column: "ProfissionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_FichaAvaliacao_Paciente_PacienteId",
                table: "FichaAvaliacao",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FichaAvaliacao_Profissional_ProfissionalId",
                table: "FichaAvaliacao",
                column: "ProfissionalId",
                principalTable: "Profissional",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_FichaAvaliacao_FichaAvaliacaoId",
                table: "Paciente",
                column: "FichaAvaliacaoId",
                principalTable: "FichaAvaliacao",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FichaAvaliacao_Paciente_PacienteId",
                table: "FichaAvaliacao");

            migrationBuilder.DropForeignKey(
                name: "FK_FichaAvaliacao_Profissional_ProfissionalId",
                table: "FichaAvaliacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_FichaAvaliacao_FichaAvaliacaoId",
                table: "Paciente");

            migrationBuilder.DropIndex(
                name: "IX_Paciente_FichaAvaliacaoId",
                table: "Paciente");

            migrationBuilder.DropIndex(
                name: "IX_FichaAvaliacao_PacienteId",
                table: "FichaAvaliacao");

            migrationBuilder.DropIndex(
                name: "IX_FichaAvaliacao_ProfissionalId",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "FichaAvaliacaoId",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "Alergias",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "Altura",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "AmplitudeMovimento",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "AssinaturaCliente",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "AssinaturaProfissional",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "AvaliacaoPostural",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "CirurgiasPrevias",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "DataAvaliacao",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "DetalheCirurgias",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "DoencasPreExistentes",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "Especialidade",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "HistoricoDoencas",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "Idade",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "Imc",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "Medicacao",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "MedicacaoUsoContinuo",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "Peso",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "ProfissionalId",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "FichaAvaliacao");

            migrationBuilder.RenameColumn(
                name: "ObservacoesGerais",
                table: "FichaAvaliacao",
                newName: "ObjetivoTratamento");

            migrationBuilder.RenameColumn(
                name: "ObjetivosDoTratamento",
                table: "FichaAvaliacao",
                newName: "Medicamentos");

            migrationBuilder.AlterColumn<string>(
                name: "TipoDor",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QueixaPrincipal",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HistoriaPregressa",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HistoriaAtual",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FrequenciaConsumoAlcool",
                table: "FichaAvaliacao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
