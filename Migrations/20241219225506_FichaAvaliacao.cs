using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class FichaAvaliacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FichaAvaliacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QueixaPrincipal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HistoriaPregressa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HistoriaAtual = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoDor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SinaisVitais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medicamentos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoencasCronicas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cirurgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoencaNeurodegenerativa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TratamentosRealizados = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlergiaMedicamentos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrequenciaConsumoAlcool = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObjetivoTratamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PraticaAtividade = table.Column<bool>(type: "bit", nullable: false),
                    Tabagista = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaAvaliacao", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FichaAvaliacao");
        }
    }
}
