using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AtualizarSalaEAdicionarAgenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Sala",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Sala",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataAlteracao",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_SalaId",
                table: "Agenda",
                column: "SalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Sala_SalaId",
                table: "Agenda",
                column: "SalaId",
                principalTable: "Sala",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_Sala_SalaId",
                table: "Agenda");

            migrationBuilder.DropIndex(
                name: "IX_Agenda_SalaId",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Sala");
        }
    }
}
