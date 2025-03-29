using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class attAgenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "RecorrenciaPacienteDto");

            migrationBuilder.AlterColumn<string>(
                name: "HoraInicio",
                table: "RecorrenciaPaciente",
                type: "text",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "interval",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HoraFim",
                table: "RecorrenciaPaciente",
                type: "text",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "interval",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FormaPagamento",
                table: "Agenda",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "Avulso",
                table: "Agenda",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraInicio",
                table: "RecorrenciaPaciente",
                type: "interval",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraFim",
                table: "RecorrenciaPaciente",
                type: "interval",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FormaPagamento",
                table: "Agenda",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
