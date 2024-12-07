using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class attFinancReceber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Financ_Receber_FormaPagamento_FormaPagamentoId",
                table: "Financ_Receber");

            migrationBuilder.DropForeignKey(
                name: "FK_Financ_Receber_TipoPagamento_TipoPagamentoId",
                table: "Financ_Receber");

            migrationBuilder.DropForeignKey(
                name: "FK_Financ_ReceberSub_Financ_Receber_Financ_ReceberId",
                table: "Financ_ReceberSub");

            migrationBuilder.DropIndex(
                name: "IX_Financ_Receber_FormaPagamentoId",
                table: "Financ_Receber");

            migrationBuilder.DropIndex(
                name: "IX_Financ_Receber_TipoPagamentoId",
                table: "Financ_Receber");

            migrationBuilder.DropColumn(
                name: "DataPagamento",
                table: "Financ_Receber");

            migrationBuilder.DropColumn(
                name: "DataVencimento",
                table: "Financ_Receber");

            migrationBuilder.DropColumn(
                name: "FormaPagamentoId",
                table: "Financ_Receber");

            migrationBuilder.DropColumn(
                name: "TipoPagamentoId",
                table: "Financ_Receber");

            migrationBuilder.RenameColumn(
                name: "ParcelaX",
                table: "Financ_ReceberSub",
                newName: "TipoPagamentoId");

            migrationBuilder.RenameColumn(
                name: "Obs",
                table: "Financ_ReceberSub",
                newName: "Observacao");

            migrationBuilder.RenameColumn(
                name: "Financ_ReceberId",
                table: "Financ_ReceberSub",
                newName: "financReceberId");

            migrationBuilder.RenameIndex(
                name: "IX_Financ_ReceberSub_Financ_ReceberId",
                table: "Financ_ReceberSub",
                newName: "IX_Financ_ReceberSub_financReceberId");

            migrationBuilder.AddColumn<int>(
                name: "FormaPagamentoId",
                table: "Financ_ReceberSub",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Parcela",
                table: "Financ_ReceberSub",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Financ_ReceberSub_FormaPagamentoId",
                table: "Financ_ReceberSub",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_ReceberSub_TipoPagamentoId",
                table: "Financ_ReceberSub",
                column: "TipoPagamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_ReceberSub_Financ_Receber_financReceberId",
                table: "Financ_ReceberSub",
                column: "financReceberId",
                principalTable: "Financ_Receber",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_ReceberSub_FormaPagamento_FormaPagamentoId",
                table: "Financ_ReceberSub",
                column: "FormaPagamentoId",
                principalTable: "FormaPagamento",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_ReceberSub_TipoPagamento_TipoPagamentoId",
                table: "Financ_ReceberSub",
                column: "TipoPagamentoId",
                principalTable: "TipoPagamento",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Financ_ReceberSub_Financ_Receber_financReceberId",
                table: "Financ_ReceberSub");

            migrationBuilder.DropForeignKey(
                name: "FK_Financ_ReceberSub_FormaPagamento_FormaPagamentoId",
                table: "Financ_ReceberSub");

            migrationBuilder.DropForeignKey(
                name: "FK_Financ_ReceberSub_TipoPagamento_TipoPagamentoId",
                table: "Financ_ReceberSub");

            migrationBuilder.DropIndex(
                name: "IX_Financ_ReceberSub_FormaPagamentoId",
                table: "Financ_ReceberSub");

            migrationBuilder.DropIndex(
                name: "IX_Financ_ReceberSub_TipoPagamentoId",
                table: "Financ_ReceberSub");

            migrationBuilder.DropColumn(
                name: "FormaPagamentoId",
                table: "Financ_ReceberSub");

            migrationBuilder.DropColumn(
                name: "Parcela",
                table: "Financ_ReceberSub");

            migrationBuilder.RenameColumn(
                name: "financReceberId",
                table: "Financ_ReceberSub",
                newName: "Financ_ReceberId");

            migrationBuilder.RenameColumn(
                name: "TipoPagamentoId",
                table: "Financ_ReceberSub",
                newName: "ParcelaX");

            migrationBuilder.RenameColumn(
                name: "Observacao",
                table: "Financ_ReceberSub",
                newName: "Obs");

            migrationBuilder.RenameIndex(
                name: "IX_Financ_ReceberSub_financReceberId",
                table: "Financ_ReceberSub",
                newName: "IX_Financ_ReceberSub_Financ_ReceberId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataPagamento",
                table: "Financ_Receber",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataVencimento",
                table: "Financ_Receber",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "FormaPagamentoId",
                table: "Financ_Receber",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoPagamentoId",
                table: "Financ_Receber",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_FormaPagamentoId",
                table: "Financ_Receber",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_TipoPagamentoId",
                table: "Financ_Receber",
                column: "TipoPagamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_Receber_FormaPagamento_FormaPagamentoId",
                table: "Financ_Receber",
                column: "FormaPagamentoId",
                principalTable: "FormaPagamento",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_Receber_TipoPagamento_TipoPagamentoId",
                table: "Financ_Receber",
                column: "TipoPagamentoId",
                principalTable: "TipoPagamento",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_ReceberSub_Financ_Receber_Financ_ReceberId",
                table: "Financ_ReceberSub",
                column: "Financ_ReceberId",
                principalTable: "Financ_Receber",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
