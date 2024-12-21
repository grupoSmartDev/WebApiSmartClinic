using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class valorPagamentoAddF_PagarEF_Receber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Financ_PagarSubModel_Financ_Pagar_financPagarId",
                table: "Financ_PagarSubModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Financ_PagarSubModel_FormaPagamento_FormaPagamentoId",
                table: "Financ_PagarSubModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Financ_PagarSubModel_TipoPagamento_TipoPagamentoId",
                table: "Financ_PagarSubModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Financ_PagarSubModel",
                table: "Financ_PagarSubModel");

            migrationBuilder.RenameTable(
                name: "Financ_PagarSubModel",
                newName: "Financ_PagarSub");

            migrationBuilder.RenameIndex(
                name: "IX_Financ_PagarSubModel_TipoPagamentoId",
                table: "Financ_PagarSub",
                newName: "IX_Financ_PagarSub_TipoPagamentoId");

            migrationBuilder.RenameIndex(
                name: "IX_Financ_PagarSubModel_FormaPagamentoId",
                table: "Financ_PagarSub",
                newName: "IX_Financ_PagarSub_FormaPagamentoId");

            migrationBuilder.RenameIndex(
                name: "IX_Financ_PagarSubModel_financPagarId",
                table: "Financ_PagarSub",
                newName: "IX_Financ_PagarSub_financPagarId");

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorPago",
                table: "Financ_ReceberSub",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorPago",
                table: "Financ_PagarSub",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Financ_PagarSub",
                table: "Financ_PagarSub",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatabaseConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EmpresaId",
                table: "Usuario",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_PagarSub_Financ_Pagar_financPagarId",
                table: "Financ_PagarSub",
                column: "financPagarId",
                principalTable: "Financ_Pagar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_PagarSub_FormaPagamento_FormaPagamentoId",
                table: "Financ_PagarSub",
                column: "FormaPagamentoId",
                principalTable: "FormaPagamento",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_PagarSub_TipoPagamento_TipoPagamentoId",
                table: "Financ_PagarSub",
                column: "TipoPagamentoId",
                principalTable: "TipoPagamento",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Empresa_EmpresaId",
                table: "Usuario",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Financ_PagarSub_Financ_Pagar_financPagarId",
                table: "Financ_PagarSub");

            migrationBuilder.DropForeignKey(
                name: "FK_Financ_PagarSub_FormaPagamento_FormaPagamentoId",
                table: "Financ_PagarSub");

            migrationBuilder.DropForeignKey(
                name: "FK_Financ_PagarSub_TipoPagamento_TipoPagamentoId",
                table: "Financ_PagarSub");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Empresa_EmpresaId",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_EmpresaId",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Financ_PagarSub",
                table: "Financ_PagarSub");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "ValorPago",
                table: "Financ_ReceberSub");

            migrationBuilder.DropColumn(
                name: "ValorPago",
                table: "Financ_PagarSub");

            migrationBuilder.RenameTable(
                name: "Financ_PagarSub",
                newName: "Financ_PagarSubModel");

            migrationBuilder.RenameIndex(
                name: "IX_Financ_PagarSub_TipoPagamentoId",
                table: "Financ_PagarSubModel",
                newName: "IX_Financ_PagarSubModel_TipoPagamentoId");

            migrationBuilder.RenameIndex(
                name: "IX_Financ_PagarSub_FormaPagamentoId",
                table: "Financ_PagarSubModel",
                newName: "IX_Financ_PagarSubModel_FormaPagamentoId");

            migrationBuilder.RenameIndex(
                name: "IX_Financ_PagarSub_financPagarId",
                table: "Financ_PagarSubModel",
                newName: "IX_Financ_PagarSubModel_financPagarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Financ_PagarSubModel",
                table: "Financ_PagarSubModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_PagarSubModel_Financ_Pagar_financPagarId",
                table: "Financ_PagarSubModel",
                column: "financPagarId",
                principalTable: "Financ_Pagar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_PagarSubModel_FormaPagamento_FormaPagamentoId",
                table: "Financ_PagarSubModel",
                column: "FormaPagamentoId",
                principalTable: "FormaPagamento",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_PagarSubModel_TipoPagamento_TipoPagamentoId",
                table: "Financ_PagarSubModel",
                column: "TipoPagamentoId",
                principalTable: "TipoPagamento",
                principalColumn: "Id");
        }
    }
}
