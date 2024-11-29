using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class ParcelasFinancReceber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Financ_ReceberSub",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Financ_ReceberId = table.Column<int>(type: "int", nullable: false),
                    ParcelaX = table.Column<int>(type: "int", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financ_ReceberSub", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Financ_ReceberSub_Financ_Receber_Financ_ReceberId",
                        column: x => x.Financ_ReceberId,
                        principalTable: "Financ_Receber",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Financ_ReceberSub_Financ_ReceberId",
                table: "Financ_ReceberSub",
                column: "Financ_ReceberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Financ_ReceberSub");
        }
    }
}
