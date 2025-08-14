using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class ativoOuNaoProfissao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Verifica se a coluna já existe antes de tentar criar
            migrationBuilder.Sql(@"
            DO $$ 
            BEGIN 
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns 
                              WHERE table_name = 'Profissao' 
                              AND column_name = 'Ativo') THEN
                    ALTER TABLE ""Profissao"" ADD ""Ativo"" boolean NOT NULL DEFAULT TRUE;
                END IF;
            END $$;
        ");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Profissao");
        }
    }
}
