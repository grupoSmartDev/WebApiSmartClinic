using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class attPlanos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plano_Profissional_ProfissionalId",
                table: "Plano");

            migrationBuilder.DropForeignKey(
                name: "FK_Plano_Sala_SalaId",
                table: "Plano");

            migrationBuilder.DropIndex(
                name: "IX_Plano_ProfissionalId",
                table: "Plano");

            migrationBuilder.DropIndex(
                name: "IX_Plano_SalaId",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "PlanoGratuito",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "ProfissionalId",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "SalaId",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "TipoCobranca",
                table: "Plano");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PlanoGratuito",
                table: "Plano",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProfissionalId",
                table: "Plano",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalaId",
                table: "Plano",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoCobranca",
                table: "Plano",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Plano_ProfissionalId",
                table: "Plano",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Plano_SalaId",
                table: "Plano",
                column: "SalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plano_Profissional_ProfissionalId",
                table: "Plano",
                column: "ProfissionalId",
                principalTable: "Profissional",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plano_Sala_SalaId",
                table: "Plano",
                column: "SalaId",
                principalTable: "Sala",
                principalColumn: "Id");
        }
    }
}
