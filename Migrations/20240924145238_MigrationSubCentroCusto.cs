using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class MigrationSubCentroCusto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CentroCustoOrigem",
                table: "CentroCusto");

            migrationBuilder.CreateTable(
                name: "SubCentroCusto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CentroCustoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCentroCusto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCentroCusto_CentroCusto_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubCentroCusto_CentroCustoId",
                table: "SubCentroCusto",
                column: "CentroCustoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubCentroCusto");

            migrationBuilder.AddColumn<string>(
                name: "CentroCustoOrigem",
                table: "CentroCusto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
