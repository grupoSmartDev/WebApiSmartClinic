using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class attFichas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FichaAvaliacao_Profissional_ProfissionalId",
                table: "FichaAvaliacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_FichaAvaliacao_FichaAvaliacaoId",
                table: "Paciente");

            migrationBuilder.DropIndex(
                name: "IX_Paciente_FichaAvaliacaoId",
                table: "Paciente");

            migrationBuilder.AddForeignKey(
                name: "FK_FichaAvaliacao_Profissional_ProfissionalId",
                table: "FichaAvaliacao",
                column: "ProfissionalId",
                principalTable: "Profissional",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FichaAvaliacao_Profissional_ProfissionalId",
                table: "FichaAvaliacao");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_FichaAvaliacaoId",
                table: "Paciente",
                column: "FichaAvaliacaoId");

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
    }
}
