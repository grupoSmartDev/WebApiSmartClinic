using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExercicio1Columnsd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Primeiro remove a chave estrangeira
            migrationBuilder.DropForeignKey(
                name: "FK_Exercicio_Evolucoes_EvolucaoId1",
                table: "Exercicio");

            // Remove o índice
            migrationBuilder.DropIndex(
                name: "IX_Exercicio_EvolucaoId1",
                table: "Exercicio");

            // Agora sim remove a coluna
            migrationBuilder.DropColumn(
                name: "EvolucaoId1",
                table: "Exercicio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
