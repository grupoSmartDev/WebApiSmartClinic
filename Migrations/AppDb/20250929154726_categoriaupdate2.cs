using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class categoriaupdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove as colunas se existirem (limpeza)
            migrationBuilder.Sql(@"
                ALTER TABLE ""Categoria"" DROP COLUMN IF EXISTS ""Ativo"";
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ""Categoria"" DROP COLUMN IF EXISTS ""IsSystemDefault"";
            ");

            // Adiciona a coluna Ativo
            migrationBuilder.Sql(@"
                ALTER TABLE ""Categoria"" ADD COLUMN ""Ativo"" boolean NOT NULL DEFAULT true;
            ");

            // Adiciona a coluna IsSystemDefault
            migrationBuilder.Sql(@"
                ALTER TABLE ""Categoria"" ADD COLUMN ""IsSystemDefault"" boolean NOT NULL DEFAULT false;
            ");

            // Atualiza a categoria "Geral" para ser IsSystemDefault se já existir
            migrationBuilder.Sql(@"
                UPDATE ""Categoria"" 
                SET ""IsSystemDefault"" = true, ""Ativo"" = true
                WHERE ""Nome"" = 'Geral';
            ");

            // Insere a categoria "Geral" apenas se não existir nenhuma com esse nome
            migrationBuilder.Sql(@"
                INSERT INTO ""Categoria"" (""Nome"", ""Ativo"", ""IsSystemDefault"")
                SELECT 'Geral', true, true
                WHERE NOT EXISTS (
                    SELECT 1 FROM ""Categoria"" 
                    WHERE ""Nome"" = 'Geral'
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove a marcação IsSystemDefault da categoria Geral
            migrationBuilder.Sql(@"
                UPDATE ""Categoria"" 
                SET ""IsSystemDefault"" = false
                WHERE ""Nome"" = 'Geral';
            ");

            // Remove as colunas
            migrationBuilder.Sql(@"
                ALTER TABLE ""Categoria"" DROP COLUMN IF EXISTS ""IsSystemDefault"";
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ""Categoria"" DROP COLUMN IF EXISTS ""Ativo"";
            ");
        }
    }
}