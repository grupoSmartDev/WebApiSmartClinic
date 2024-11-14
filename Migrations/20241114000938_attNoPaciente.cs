using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class attNoPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeDaEmpresa",
                table: "Paciente",
                newName: "Uf");

            migrationBuilder.RenameColumn(
                name: "Medico",
                table: "Paciente",
                newName: "ProfissionalId");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Paciente",
                newName: "PlanoId");

            migrationBuilder.AddColumn<int>(
                name: "PlanoId1",
                table: "Paciente",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfissionalId1",
                table: "Paciente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_PlanoId1",
                table: "Paciente",
                column: "PlanoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_ProfissionalId1",
                table: "Paciente",
                column: "ProfissionalId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_Plano_PlanoId1",
                table: "Paciente",
                column: "PlanoId1",
                principalTable: "Plano",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_Profissional_ProfissionalId1",
                table: "Paciente",
                column: "ProfissionalId1",
                principalTable: "Profissional",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_Plano_PlanoId1",
                table: "Paciente");

            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_Profissional_ProfissionalId1",
                table: "Paciente");

            migrationBuilder.DropIndex(
                name: "IX_Paciente_PlanoId1",
                table: "Paciente");

            migrationBuilder.DropIndex(
                name: "IX_Paciente_ProfissionalId1",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "PlanoId1",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "ProfissionalId1",
                table: "Paciente");

            migrationBuilder.RenameColumn(
                name: "Uf",
                table: "Paciente",
                newName: "NomeDaEmpresa");

            migrationBuilder.RenameColumn(
                name: "ProfissionalId",
                table: "Paciente",
                newName: "Medico");

            migrationBuilder.RenameColumn(
                name: "PlanoId",
                table: "Paciente",
                newName: "Estado");
        }
    }
}
