using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class FixForeignKeyRecorrencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecorrenciaPacienteDto_Paciente_PacienteModelId",
                table: "RecorrenciaPacienteDto");

            migrationBuilder.DropIndex(
                name: "IX_RecorrenciaPacienteDto_PacienteModelId",
                table: "RecorrenciaPacienteDto");

            migrationBuilder.DropColumn(
                name: "PacienteModelId",
                table: "RecorrenciaPacienteDto");

            migrationBuilder.AddColumn<bool>(
                name: "IsSystemDefault",
                table: "TipoPagamento",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSystemDefault",
                table: "Status",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSystemDefault",
                table: "Profissao",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSystemDefault",
                table: "FormaPagamento",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSystemDefault",
                table: "Convenio",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSystemDefault",
                table: "Conselho",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Conselho",
                columns: new[] { "Id", "IsSystemDefault", "Nome", "Sigla" },
                values: new object[] { 1, true, "Conselho Regional de Fisioterapia e Terapia Ocupacional", "CREFITO" });

            migrationBuilder.InsertData(
                table: "Convenio",
                columns: new[] { "Id", "Ativo", "Email", "IsSystemDefault", "Nome", "PeriodoCarencia", "RegistroAvs", "Telefone" },
                values: new object[,]
                {
                    { 1, true, "email@email.com", true, "Unimed", "0", "ABC", "3434-3434" },
                    { 2, true, "email@email.com", true, "Santa Casa", "0", "ABC", "3434-3434" }
                });

            migrationBuilder.InsertData(
                table: "FormaPagamento",
                columns: new[] { "Id", "Descricao", "IsSystemDefault", "Parcelas" },
                values: new object[,]
                {
                    { 1, "À Vista", true, 1 },
                    { 2, "A Prazo - 2x", true, 2 },
                    { 3, "A Prazo - 3x", true, 3 }
                });

            migrationBuilder.InsertData(
                table: "Profissao",
                columns: new[] { "Id", "Descricao", "IsSystemDefault" },
                values: new object[] { 1, "Psicólogo", true });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Cor", "IsSystemDefault", "Legenda", "Status" },
                values: new object[] { 1, "#00FF00", true, "Agendamento realizado com sucesso!", "Agendado" });

            migrationBuilder.InsertData(
                table: "TipoPagamento",
                columns: new[] { "Id", "Descricao", "IsSystemDefault" },
                values: new object[,]
                {
                    { 1, "Dinheiro", true },
                    { 2, "Cartão de Crédito", true },
                    { 3, "Cartão de Débito", true },
                    { 4, "Boleto", true },
                    { 5, "Pix", true },
                    { 6, "Depósito", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecorrenciaPacienteDto_PacienteId",
                table: "RecorrenciaPacienteDto",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecorrenciaPacienteDto_Paciente_PacienteId",
                table: "RecorrenciaPacienteDto",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecorrenciaPacienteDto_Paciente_PacienteId",
                table: "RecorrenciaPacienteDto");

            migrationBuilder.DropIndex(
                name: "IX_RecorrenciaPacienteDto_PacienteId",
                table: "RecorrenciaPacienteDto");

            migrationBuilder.DeleteData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Convenio",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Convenio",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "IsSystemDefault",
                table: "TipoPagamento");

            migrationBuilder.DropColumn(
                name: "IsSystemDefault",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "IsSystemDefault",
                table: "Profissao");

            migrationBuilder.DropColumn(
                name: "IsSystemDefault",
                table: "FormaPagamento");

            migrationBuilder.DropColumn(
                name: "IsSystemDefault",
                table: "Convenio");

            migrationBuilder.DropColumn(
                name: "IsSystemDefault",
                table: "Conselho");

            //migrationBuilder.AddColumn<int>(
            //    name: "PacienteModelId",
            //    table: "RecorrenciaPacienteDto",
            //    type: "integer",
            //    nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecorrenciaPacienteDto_PacienteModelId",
                table: "RecorrenciaPacienteDto",
                column: "PacienteModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecorrenciaPacienteDto_Paciente_PacienteModelId",
                table: "RecorrenciaPacienteDto",
                column: "PacienteModelId",
                principalTable: "Paciente",
                principalColumn: "Id");
        }
    }
}
