using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class PlanoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plano",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TempoMinutos = table.Column<int>(type: "int", nullable: false),
                    CentroCustoId = table.Column<int>(type: "int", nullable: true),
                    TipoCobranca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanoGratuito = table.Column<bool>(type: "bit", nullable: false),
                    ValorPlano = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalaId = table.Column<int>(type: "int", nullable: true),
                    ProfissionalId = table.Column<int>(type: "int", nullable: true),
                    PlanoBimestral = table.Column<bool>(type: "bit", nullable: false),
                    ValorMesBimestral = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorTotalBimestral = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DescontoMesBimestral = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PlanoTrimestral = table.Column<bool>(type: "bit", nullable: false),
                    ValorMesTrimestral = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorTotalTrimestral = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DescontoMesTrimestral = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PlanoQuadrimestral = table.Column<bool>(type: "bit", nullable: false),
                    ValorMesQuadrimestral = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorTotalQuadrimestral = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DescontoMesQuadrimestral = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PlanoSemestral = table.Column<bool>(type: "bit", nullable: false),
                    ValorMesSemestral = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorTotalSemestral = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DescontoMesSemestral = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PlanoAnual = table.Column<bool>(type: "bit", nullable: false),
                    ValorMesAnual = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorTotalAnual = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DescontoMesAnual = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plano", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plano_CentroCusto_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Plano_Profissional_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissional",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Plano_Sala_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sala",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plano_CentroCustoId",
                table: "Plano",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_Plano_ProfissionalId",
                table: "Plano",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Plano_SalaId",
                table: "Plano",
                column: "SalaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plano");
        }
    }
}
