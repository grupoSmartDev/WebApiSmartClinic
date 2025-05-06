using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class alteracaoVinculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Profissional_ConselhoId",
                table: "Profissional",
                column: "ConselhoId");

            migrationBuilder.CreateIndex(
                name: "IX_Profissional_ProfissaoId",
                table: "Profissional",
                column: "ProfissaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profissional_Conselho_ConselhoId",
                table: "Profissional",
                column: "ConselhoId",
                principalTable: "Conselho",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profissional_Profissao_ProfissaoId",
                table: "Profissional",
                column: "ProfissaoId",
                principalTable: "Profissao",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profissional_Conselho_ConselhoId",
                table: "Profissional");

            migrationBuilder.DropForeignKey(
                name: "FK_Profissional_Profissao_ProfissaoId",
                table: "Profissional");

            migrationBuilder.DropIndex(
                name: "IX_Profissional_ConselhoId",
                table: "Profissional");

            migrationBuilder.DropIndex(
                name: "IX_Profissional_ProfissaoId",
                table: "Profissional");
        }
    }
}
