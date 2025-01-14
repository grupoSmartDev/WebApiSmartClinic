using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class AddEvolucaoPacienteRelationship2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EvolucaoId",
                table: "Exercicio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EvolucaoId1",
                table: "Exercicio",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EvolucaoId1",
                table: "Atividade",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercicio_EvolucaoId1",
                table: "Exercicio",
                column: "EvolucaoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Atividade_EvolucaoId1",
                table: "Atividade",
                column: "EvolucaoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividade_Evolucoes_EvolucaoId1",
                table: "Atividade",
                column: "EvolucaoId1",
                principalTable: "Evolucoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercicio_Evolucoes_EvolucaoId1",
                table: "Exercicio",
                column: "EvolucaoId1",
                principalTable: "Evolucoes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividade_Evolucoes_EvolucaoId1",
                table: "Atividade");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercicio_Evolucoes_EvolucaoId1",
                table: "Exercicio");

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

            migrationBuilder.AlterColumn<int>(
                name: "EvolucaoId",
                table: "Exercicio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
