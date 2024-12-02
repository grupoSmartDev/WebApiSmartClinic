using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class atualizacoesGerals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividade_Evolucao_EvolucaoModelId",
                table: "Atividade");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercicio_Evolucao_EvolucaoModelId",
                table: "Exercicio");

            migrationBuilder.DropForeignKey(
                name: "FK_Plano_CentroCusto_CentroCustoId",
                table: "Plano");

            migrationBuilder.DropIndex(
                name: "IX_Plano_CentroCustoId",
                table: "Plano");

            migrationBuilder.DropIndex(
                name: "IX_Paciente_PlanoId",
                table: "Paciente");

            migrationBuilder.DropIndex(
                name: "IX_Exercicio_EvolucaoModelId",
                table: "Exercicio");

            migrationBuilder.DropColumn(
                name: "DescontoMesAnual",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "DescontoMesBimestral",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "DescontoMesQuadrimestral",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "DescontoMesSemestral",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "DescontoMesTrimestral",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "PlanoAnual",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "PlanoBimestral",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "PlanoQuadrimestral",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "PlanoSemestral",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "ValorMesAnual",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "ValorMesBimestral",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "ValorMesQuadrimestral",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "ValorMesSemestral",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "ValorPlano",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "EvolucaoModelId",
                table: "Exercicio");

            migrationBuilder.RenameColumn(
                name: "ValorTotalTrimestral",
                table: "Plano",
                newName: "ValorTrimestral");

            migrationBuilder.RenameColumn(
                name: "ValorTotalSemestral",
                table: "Plano",
                newName: "ValorSemestral");

            migrationBuilder.RenameColumn(
                name: "ValorTotalQuadrimestral",
                table: "Plano",
                newName: "ValorQuadrimestral");

            migrationBuilder.RenameColumn(
                name: "ValorTotalBimestral",
                table: "Plano",
                newName: "ValorMensal");

            migrationBuilder.RenameColumn(
                name: "ValorTotalAnual",
                table: "Plano",
                newName: "ValorBimestral");

            migrationBuilder.RenameColumn(
                name: "ValorMesTrimestral",
                table: "Plano",
                newName: "ValorAnual");

            migrationBuilder.RenameColumn(
                name: "PlanoTrimestral",
                table: "Plano",
                newName: "Ativo");

            migrationBuilder.RenameColumn(
                name: "Obs",
                table: "Evolucao",
                newName: "Observacao");

            migrationBuilder.RenameColumn(
                name: "EvolucaoModelId",
                table: "Atividade",
                newName: "EvolucaoId1");

            migrationBuilder.RenameIndex(
                name: "IX_Atividade_EvolucaoModelId",
                table: "Atividade",
                newName: "IX_Atividade_EvolucaoId1");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Plano",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "Plano",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicio",
                table: "Plano",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiasSemana",
                table: "Plano",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FinanceiroId",
                table: "Plano",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "Plano",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoMes",
                table: "Plano",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataUltimoAtendimento",
                table: "Paciente",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EvolucaoId1",
                table: "Exercicio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PacienteId",
                table: "Evolucao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EvolucaoId",
                table: "Atividade",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Evolucoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEvolucao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evolucoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evolucoes_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plano_FinanceiroId",
                table: "Plano",
                column: "FinanceiroId");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_PlanoId",
                table: "Paciente",
                column: "PlanoId",
                unique: true,
                filter: "[PlanoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Exercicio_EvolucaoId",
                table: "Exercicio",
                column: "EvolucaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercicio_EvolucaoId1",
                table: "Exercicio",
                column: "EvolucaoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Evolucao_PacienteId",
                table: "Evolucao",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Atividade_EvolucaoId",
                table: "Atividade",
                column: "EvolucaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolucoes_PacienteId",
                table: "Evolucoes",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividade_Evolucao_EvolucaoId1",
                table: "Atividade",
                column: "EvolucaoId1",
                principalTable: "Evolucao",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividade_Evolucoes_EvolucaoId",
                table: "Atividade",
                column: "EvolucaoId",
                principalTable: "Evolucoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evolucao_Paciente_PacienteId",
                table: "Evolucao",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercicio_Evolucao_EvolucaoId1",
                table: "Exercicio",
                column: "EvolucaoId1",
                principalTable: "Evolucao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercicio_Evolucoes_EvolucaoId",
                table: "Exercicio",
                column: "EvolucaoId",
                principalTable: "Evolucoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Plano_Financ_Receber_FinanceiroId",
                table: "Plano",
                column: "FinanceiroId",
                principalTable: "Financ_Receber",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividade_Evolucao_EvolucaoId1",
                table: "Atividade");

            migrationBuilder.DropForeignKey(
                name: "FK_Atividade_Evolucoes_EvolucaoId",
                table: "Atividade");

            migrationBuilder.DropForeignKey(
                name: "FK_Evolucao_Paciente_PacienteId",
                table: "Evolucao");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercicio_Evolucao_EvolucaoId1",
                table: "Exercicio");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercicio_Evolucoes_EvolucaoId",
                table: "Exercicio");

            migrationBuilder.DropForeignKey(
                name: "FK_Plano_Financ_Receber_FinanceiroId",
                table: "Plano");

            migrationBuilder.DropTable(
                name: "Evolucoes");

            migrationBuilder.DropIndex(
                name: "IX_Plano_FinanceiroId",
                table: "Plano");

            migrationBuilder.DropIndex(
                name: "IX_Paciente_PlanoId",
                table: "Paciente");

            migrationBuilder.DropIndex(
                name: "IX_Exercicio_EvolucaoId",
                table: "Exercicio");

            migrationBuilder.DropIndex(
                name: "IX_Exercicio_EvolucaoId1",
                table: "Exercicio");

            migrationBuilder.DropIndex(
                name: "IX_Evolucao_PacienteId",
                table: "Evolucao");

            migrationBuilder.DropIndex(
                name: "IX_Atividade_EvolucaoId",
                table: "Atividade");

            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "DataInicio",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "DiasSemana",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "FinanceiroId",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "TipoMes",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "DataUltimoAtendimento",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "EvolucaoId1",
                table: "Exercicio");

            migrationBuilder.RenameColumn(
                name: "ValorTrimestral",
                table: "Plano",
                newName: "ValorTotalTrimestral");

            migrationBuilder.RenameColumn(
                name: "ValorSemestral",
                table: "Plano",
                newName: "ValorTotalSemestral");

            migrationBuilder.RenameColumn(
                name: "ValorQuadrimestral",
                table: "Plano",
                newName: "ValorTotalQuadrimestral");

            migrationBuilder.RenameColumn(
                name: "ValorMensal",
                table: "Plano",
                newName: "ValorTotalBimestral");

            migrationBuilder.RenameColumn(
                name: "ValorBimestral",
                table: "Plano",
                newName: "ValorTotalAnual");

            migrationBuilder.RenameColumn(
                name: "ValorAnual",
                table: "Plano",
                newName: "ValorMesTrimestral");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "Plano",
                newName: "PlanoTrimestral");

            migrationBuilder.RenameColumn(
                name: "Observacao",
                table: "Evolucao",
                newName: "Obs");

            migrationBuilder.RenameColumn(
                name: "EvolucaoId1",
                table: "Atividade",
                newName: "EvolucaoModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Atividade_EvolucaoId1",
                table: "Atividade",
                newName: "IX_Atividade_EvolucaoModelId");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Plano",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<decimal>(
                name: "DescontoMesAnual",
                table: "Plano",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DescontoMesBimestral",
                table: "Plano",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DescontoMesQuadrimestral",
                table: "Plano",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DescontoMesSemestral",
                table: "Plano",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DescontoMesTrimestral",
                table: "Plano",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PlanoAnual",
                table: "Plano",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PlanoBimestral",
                table: "Plano",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PlanoQuadrimestral",
                table: "Plano",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PlanoSemestral",
                table: "Plano",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorMesAnual",
                table: "Plano",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorMesBimestral",
                table: "Plano",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorMesQuadrimestral",
                table: "Plano",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorMesSemestral",
                table: "Plano",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorPlano",
                table: "Plano",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EvolucaoModelId",
                table: "Exercicio",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PacienteId",
                table: "Evolucao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EvolucaoId",
                table: "Atividade",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plano_CentroCustoId",
                table: "Plano",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_PlanoId",
                table: "Paciente",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercicio_EvolucaoModelId",
                table: "Exercicio",
                column: "EvolucaoModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividade_Evolucao_EvolucaoModelId",
                table: "Atividade",
                column: "EvolucaoModelId",
                principalTable: "Evolucao",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercicio_Evolucao_EvolucaoModelId",
                table: "Exercicio",
                column: "EvolucaoModelId",
                principalTable: "Evolucao",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plano_CentroCusto_CentroCustoId",
                table: "Plano",
                column: "CentroCustoId",
                principalTable: "CentroCusto",
                principalColumn: "Id");
        }
    }
}
