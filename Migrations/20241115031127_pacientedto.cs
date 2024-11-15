using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class pacientedto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "Financ_Receber",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Agenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraFim = table.Column<TimeSpan>(type: "time", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    ProfissionalId = table.Column<int>(type: "int", nullable: false),
                    Convenio = table.Column<bool>(type: "bit", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FormaPagamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pago = table.Column<bool>(type: "bit", nullable: false),
                    FinanceiroId = table.Column<int>(type: "int", nullable: true),
                    SalaId = table.Column<int>(type: "int", nullable: false),
                    PacoteId = table.Column<int>(type: "int", nullable: true),
                    LembreteSms = table.Column<bool>(type: "bit", nullable: false),
                    LembreteWhatsapp = table.Column<bool>(type: "bit", nullable: false),
                    LembreteEmail = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntegracaoGmail = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agenda", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_PacienteId",
                table: "Financ_Receber",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_Receber_Paciente_PacienteId",
                table: "Financ_Receber",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Financ_Receber_Paciente_PacienteId",
                table: "Financ_Receber");

            migrationBuilder.DropTable(
                name: "Agenda");

            migrationBuilder.DropIndex(
                name: "IX_Financ_Receber_PacienteId",
                table: "Financ_Receber");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Financ_Receber");
        }
    }
}
