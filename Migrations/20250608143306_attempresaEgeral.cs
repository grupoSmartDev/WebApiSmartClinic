using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class attempresaEgeral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove a constraint apenas se ela existir
            migrationBuilder.Sql(@"
        DO $$ 
        BEGIN
            IF EXISTS (SELECT 1 FROM information_schema.table_constraints 
                       WHERE constraint_name = 'FK_Financ_Pagar_DespesasFixas_DespesaFixaId') THEN
                ALTER TABLE ""Financ_Pagar"" DROP CONSTRAINT ""FK_Financ_Pagar_DespesasFixas_DespesaFixaId"";
            END IF;
        END $$;
    ");

            // Remove a coluna apenas se ela existir
            migrationBuilder.Sql(@"
        DO $$ 
        BEGIN
            IF EXISTS (SELECT 1 FROM information_schema.columns 
                       WHERE table_name = 'DespesasFixas' AND column_name = 'FinancPagarId') THEN
                ALTER TABLE ""DespesasFixas"" DROP COLUMN ""FinancPagarId"";
            END IF;
        END $$;
    ");

            // Adiciona a coluna DespesaFixaId na tabela Financ_Pagar se ela não existir
            migrationBuilder.Sql(@"
        DO $$ 
        BEGIN
            IF NOT EXISTS (SELECT 1 FROM information_schema.columns 
                          WHERE table_name = 'Financ_Pagar' AND column_name = 'DespesaFixaId') THEN
                ALTER TABLE ""Financ_Pagar"" ADD COLUMN ""DespesaFixaId"" integer;
            END IF;
        END $$;
    ");

            // Adiciona a nova coluna DataAlteracao
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "DespesasFixas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            // Adiciona a foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_Financ_Pagar_DespesasFixas_DespesaFixaId",
                table: "Financ_Pagar",
                column: "DespesaFixaId",
                principalTable: "DespesasFixas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "DespesasFixas");

            migrationBuilder.AddColumn<int>(
                name: "FinancPagarId",
                table: "DespesasFixas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DespesasFixas_FinancPagarId",
                table: "DespesasFixas",
                column: "FinancPagarId");

            migrationBuilder.AddForeignKey(
                name: "FK_DespesasFixas_Financ_Pagar_FinancPagarId",
                table: "DespesasFixas",
                column: "FinancPagarId",
                principalTable: "Financ_Pagar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_Pagar_DespesasFixas_DespesaFixaId",
                table: "Financ_Pagar",
                column: "DespesaFixaId",
                principalTable: "DespesasFixas",
                principalColumn: "Id");
        }
    }
}
