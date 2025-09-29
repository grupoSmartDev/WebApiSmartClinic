using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class categoriaupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Adiciona a coluna Ativo apenas se não existir
            migrationBuilder.Sql(@"
                DO $$ 
                BEGIN
                    IF NOT EXISTS (
                        SELECT 1 FROM information_schema.columns 
                        WHERE table_name = 'Categoria' AND column_name = 'Ativo'
                    ) THEN
                        ALTER TABLE ""Categoria"" ADD COLUMN ""Ativo"" boolean NOT NULL DEFAULT true;
                    END IF;
                END $$;
            ");

            // Adiciona a coluna IsSystemDefault apenas se não existir
            migrationBuilder.Sql(@"
                DO $$ 
                BEGIN
                    IF NOT EXISTS (
                        SELECT 1 FROM information_schema.columns 
                        WHERE table_name = 'Categoria' AND column_name = 'IsSystemDefault'
                    ) THEN
                        ALTER TABLE ""Categoria"" ADD COLUMN ""IsSystemDefault"" boolean NOT NULL DEFAULT false;
                    END IF;
                END $$;
            ");

            // Atualiza registros existentes para ficarem ativos (caso a coluna já existisse)
            migrationBuilder.Sql(@"
                UPDATE ""Categoria"" 
                SET ""Ativo"" = true 
                WHERE ""Ativo"" = false OR ""Ativo"" IS NULL;
            ");

            // Insere a categoria padrão se não existir
            migrationBuilder.Sql(@"
                INSERT INTO ""Categoria"" (""Nome"", ""Ativo"", ""IsSystemDefault"")
                SELECT 'Geral', true, true
                WHERE NOT EXISTS (
                    SELECT 1 FROM ""Categoria"" 
                    WHERE ""Nome"" = 'Geral' AND ""IsSystemDefault"" = true
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove a categoria padrão
            migrationBuilder.Sql(@"
                DELETE FROM ""Categoria"" 
                WHERE ""Nome"" = 'Geral' AND ""IsSystemDefault"" = true;
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