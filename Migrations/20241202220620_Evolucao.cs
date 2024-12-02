using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class Evolucao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EvolucaoModelId",
                table: "Exercicio",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EvolucaoModelId",
                table: "Atividade",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Evolucao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEvolucao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evolucao", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercicio_EvolucaoModelId",
                table: "Exercicio",
                column: "EvolucaoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Atividade_EvolucaoModelId",
                table: "Atividade",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividade_Evolucao_EvolucaoModelId",
                table: "Atividade");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercicio_Evolucao_EvolucaoModelId",
                table: "Exercicio");

            migrationBuilder.DropTable(
                name: "Evolucao");

            migrationBuilder.DropIndex(
                name: "IX_Exercicio_EvolucaoModelId",
                table: "Exercicio");

            migrationBuilder.DropIndex(
                name: "IX_Atividade_EvolucaoModelId",
                table: "Atividade");

            migrationBuilder.DropColumn(
                name: "EvolucaoModelId",
                table: "Exercicio");

            migrationBuilder.DropColumn(
                name: "EvolucaoModelId",
                table: "Atividade");
        }
    }
}
