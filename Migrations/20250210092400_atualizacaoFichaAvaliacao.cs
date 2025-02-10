using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class atualizacaoFichaAvaliacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ObjetivoTratamento",
                table: "FichaAvaliacao",
                newName: "Sexo");

            migrationBuilder.RenameColumn(
                name: "Medicamentos",
                table: "FichaAvaliacao",
                newName: "ObservacoesGerais");

            migrationBuilder.AddColumn<int>(
                name: "FichaAvaliacaoId",
                table: "Paciente",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TipoDor",
                table: "FichaAvaliacao",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "QueixaPrincipal",
                table: "FichaAvaliacao",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "HistoriaPregressa",
                table: "FichaAvaliacao",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "HistoriaAtual",
                table: "FichaAvaliacao",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FrequenciaConsumoAlcool",
                table: "FichaAvaliacao",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Alergias",
                table: "FichaAvaliacao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Altura",
                table: "FichaAvaliacao",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmplitudeMovimento",
                table: "FichaAvaliacao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssinaturaCliente",
                table: "FichaAvaliacao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssinaturaProfissional",
                table: "FichaAvaliacao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvaliacaoPostural",
                table: "FichaAvaliacao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CirurgiasPrevias",
                table: "FichaAvaliacao",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAvaliacao",
                table: "FichaAvaliacao",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetalheCirurgias",
                table: "FichaAvaliacao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoencasPreExistentes",
                table: "FichaAvaliacao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Especialidade",
                table: "FichaAvaliacao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HistoricoDoencas",
                table: "FichaAvaliacao",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "IMC",
                table: "FichaAvaliacao",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "FichaAvaliacao",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medicacao",
                table: "FichaAvaliacao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MedicacaoUsoContinuo",
                table: "FichaAvaliacao",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ObjetivosDoTratamento",
                table: "FichaAvaliacao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "FichaAvaliacao",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Peso",
                table: "FichaAvaliacao",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfissionalId",
                table: "FichaAvaliacao",
                type: "integer",
                nullable: true);

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
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FichaAvaliacao_Profissional_ProfissionalId",
                table: "FichaAvaliacao",
                column: "ProfissionalId",
                principalTable: "Profissional",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
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
                name: "IMC",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "Idade",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "Medicacao",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "MedicacaoUsoContinuo",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "ObjetivosDoTratamento",
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

            migrationBuilder.RenameColumn(
                name: "Sexo",
                table: "FichaAvaliacao",
                newName: "ObjetivoTratamento");

            migrationBuilder.RenameColumn(
                name: "ObservacoesGerais",
                table: "FichaAvaliacao",
                newName: "Medicamentos");

            migrationBuilder.AlterColumn<string>(
                name: "TipoDor",
                table: "FichaAvaliacao",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QueixaPrincipal",
                table: "FichaAvaliacao",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HistoriaPregressa",
                table: "FichaAvaliacao",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HistoriaAtual",
                table: "FichaAvaliacao",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FrequenciaConsumoAlcool",
                table: "FichaAvaliacao",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
