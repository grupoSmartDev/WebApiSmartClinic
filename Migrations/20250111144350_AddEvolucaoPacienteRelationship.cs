using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class AddEvolucaoPacienteRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividade_Evolucao_EvolucaoId1",
                table: "Atividade");

            migrationBuilder.DropForeignKey(
                name: "FK_Evolucoes_Paciente_PacienteId",
                table: "Evolucoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercicio_Evolucao_EvolucaoId1",
                table: "Exercicio");

            migrationBuilder.DropTable(
                name: "Evolucao");

            migrationBuilder.DropIndex(
                name: "IX_Exercicio_EvolucaoId1",
                table: "Exercicio");

            migrationBuilder.DropIndex(
                name: "IX_Atividade_EvolucaoId1",
                table: "Atividade");

            migrationBuilder.DropColumn(
                name: "EvolucaoId1",
                table: "Exercicio");

            migrationBuilder.DropColumn(
                name: "EvolucaoId1",
                table: "Atividade");

            migrationBuilder.AddForeignKey(
                name: "FK_Evolucoes_Paciente_PacienteId",
                table: "Evolucoes",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evolucoes_Paciente_PacienteId",
                table: "Evolucoes");

            migrationBuilder.AddColumn<int>(
                name: "EvolucaoId1",
                table: "Exercicio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EvolucaoId1",
                table: "Atividade",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Evolucao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(type: "int", nullable: true),
                    DataEvolucao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evolucao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evolucao_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercicio_EvolucaoId1",
                table: "Exercicio",
                column: "EvolucaoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Atividade_EvolucaoId1",
                table: "Atividade",
                column: "EvolucaoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Evolucao_PacienteId",
                table: "Evolucao",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividade_Evolucao_EvolucaoId1",
                table: "Atividade",
                column: "EvolucaoId1",
                principalTable: "Evolucao",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evolucoes_Paciente_PacienteId",
                table: "Evolucoes",
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
        }
    }
}
