using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class planos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_Plano_PlanoId",
                table: "Paciente");

            migrationBuilder.DropForeignKey(
                name: "FK_Plano_Paciente_PacienteId",
                table: "Plano");

            migrationBuilder.DropIndex(
                name: "IX_Plano_PacienteId",
                table: "Plano");

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_Plano_PlanoId",
                table: "Paciente",
                column: "PlanoId",
                principalTable: "Plano",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_Plano_PlanoId",
                table: "Paciente");

            migrationBuilder.CreateIndex(
                name: "IX_Plano_PacienteId",
                table: "Plano",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_Plano_PlanoId",
                table: "Paciente",
                column: "PlanoId",
                principalTable: "Plano",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plano_Paciente_PacienteId",
                table: "Plano",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id");
        }
    }
}
