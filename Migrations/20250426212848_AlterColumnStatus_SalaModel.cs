using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class AlterColumnStatus_SalaModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Passo 1: Atualizar valores existentes para TRUE/FALSE
            migrationBuilder.Sql("UPDATE \"Sala\" SET \"Status\" = 'TRUE' WHERE \"Status\" ILIKE 'true' OR \"Status\" ILIKE 'ativo';");
            migrationBuilder.Sql("UPDATE \"Sala\" SET \"Status\" = 'FALSE' WHERE \"Status\" IS NULL OR \"Status\" ILIKE 'false' OR \"Status\" ILIKE 'inativo';");

            // Passo 2: Alterar o tipo da coluna para boolean
            migrationBuilder.Sql("ALTER TABLE \"Sala\" ALTER COLUMN \"Status\" TYPE boolean USING (\"Status\"::boolean);");

            // Passo 3: Tornar NOT NULL e definir valor padrão
            migrationBuilder.Sql("ALTER TABLE \"Sala\" ALTER COLUMN \"Status\" SET NOT NULL;");
            migrationBuilder.Sql("ALTER TABLE \"Sala\" ALTER COLUMN \"Status\" SET DEFAULT FALSE;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Sala",
                type: "text",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.UpdateData(
                table: "Sala",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: "Ativo");
        }
    }
}
