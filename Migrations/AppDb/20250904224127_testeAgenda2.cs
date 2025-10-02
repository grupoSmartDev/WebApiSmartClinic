using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class testeAgenda2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DO $$
            BEGIN
                IF NOT EXISTS (
                    SELECT 1
                    FROM information_schema.columns
                    WHERE table_name='Agenda' AND column_name='Ativo'
                ) THEN
                    ALTER TABLE ""Agenda"" ADD COLUMN ""Ativo"" boolean NOT NULL DEFAULT TRUE;
                END IF;
            END $$;
        ");

            migrationBuilder.Sql(@"
    DO $$
    BEGIN
        IF NOT EXISTS (
            SELECT 1
            FROM information_schema.columns
            WHERE table_name='Agenda' AND column_name='DataAlteracao'
        ) THEN
            ALTER TABLE ""Agenda"" ADD COLUMN ""DataAlteracao"" timestamp with time zone NULL;
        END IF;
    END $$;
");


            migrationBuilder.Sql(@"
    DO $$
    BEGIN
        IF NOT EXISTS (
            SELECT 1
            FROM information_schema.columns
            WHERE table_name='Agenda' AND column_name='DataCriacao'
        ) THEN
            ALTER TABLE ""Agenda"" ADD COLUMN ""DataCriacao"" timestamp with time zone NOT NULL DEFAULT '0001-01-01 00:00:00';
        END IF;
    END $$;
");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Agenda");
        }
    }
}
