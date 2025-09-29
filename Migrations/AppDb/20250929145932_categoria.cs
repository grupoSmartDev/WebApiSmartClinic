using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class categoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Adiciona apenas IsSystemDefault (se Ativo já existe)
            migrationBuilder.AddColumn<bool>(
                name: "IsSystemDefault",
                table: "Categoria",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            // Atualiza registros existentes para Ativo = true (se necessário)
            migrationBuilder.Sql("UPDATE \"Categoria\" SET \"Ativo\" = true WHERE \"Ativo\" = false;");

            // Insere categoria padrão se não existir
            migrationBuilder.Sql(@"
                INSERT INTO ""Categoria"" (""Id"", ""Ativo"", ""IsSystemDefault"", ""Nome"")
                SELECT 1, true, true, 'Geral'
                WHERE NOT EXISTS (SELECT 1 FROM ""Categoria"" WHERE ""Id"" = 1);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "IsSystemDefault",
                table: "Categoria");
        }
    }
}