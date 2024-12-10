using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class attfinancreceberesub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desconto",
                table: "Financ_Receber");

            migrationBuilder.DropColumn(
                name: "Juros",
                table: "Financ_Receber");

            migrationBuilder.DropColumn(
                name: "Multa",
                table: "Financ_Receber");

            migrationBuilder.AddColumn<decimal>(
                name: "Desconto",
                table: "Financ_ReceberSub",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Juros",
                table: "Financ_ReceberSub",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Multa",
                table: "Financ_ReceberSub",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desconto",
                table: "Financ_ReceberSub");

            migrationBuilder.DropColumn(
                name: "Juros",
                table: "Financ_ReceberSub");

            migrationBuilder.DropColumn(
                name: "Multa",
                table: "Financ_ReceberSub");

            migrationBuilder.AddColumn<decimal>(
                name: "Desconto",
                table: "Financ_Receber",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Juros",
                table: "Financ_Receber",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Multa",
                table: "Financ_Receber",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
