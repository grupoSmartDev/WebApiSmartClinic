using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class attFormaPagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormaPagamentoId",
                table: "DespesasFixas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoPagamentoId",
                table: "DespesasFixas",
                type: "integer",
                nullable: true);


            migrationBuilder.CreateIndex(
                name: "IX_DespesasFixas_FormaPagamentoId",
                table: "DespesasFixas",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasFixas_TipoPagamentoId",
                table: "DespesasFixas",
                column: "TipoPagamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DespesasFixas_FormaPagamento_FormaPagamentoId",
                table: "DespesasFixas",
                column: "FormaPagamentoId",
                principalTable: "FormaPagamento",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DespesasFixas_TipoPagamento_TipoPagamentoId",
                table: "DespesasFixas",
                column: "TipoPagamentoId",
                principalTable: "TipoPagamento",
                principalColumn: "Id");

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
