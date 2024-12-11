using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class attfinancreceberesubsadasdsdada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "Financ_Receber",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Financ_Receber");
        }
    }
}
